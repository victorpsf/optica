using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Databases.Entities;
using Shared.Dtos.Input.AuthenticationModules;
using Shared.Dtos.Output;
using Shared.Interfaces.Configurations;
using Shared.Interfaces.Databases;
using Shared.Libraries;
using Shared.Models.Security;
using Shared.Security;
using Shared.Services;
using Shared.Connectors.Interfaces;
using Microsoft.AspNetCore.Cors;
using Shared.Dtos.Input;

namespace Authentication.Server;

[AllowAnonymous]
[EnableCors("MyPolicy")]
public partial class AuthenticationController: ControllerBase, IAuthenticationConnector<IActionResult>
{
    private ISecurityConfiguration securityConfiguration;
    private IUserService service;
    private IAuthCodeService authCodeService;
    private SmtpService smtpService;
    private HostCache hostCache;

    public AuthenticationController(
        ISecurityConfiguration securityConfiguration,
        IUserService service,
        IAuthCodeService authCodeService,
        SmtpService smtpService,
        HostCache hostCache
    ) {
        this.service = service;
        this.securityConfiguration = securityConfiguration;
        this.authCodeService = authCodeService;
        this.smtpService = smtpService;
        this.hostCache = hostCache;
    }

    [HttpPost]
    public async Task<IActionResult> Index(
        [FromBody] Autentication autentication
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = this.service.FindByEmailOrName(
            autentication.Email,
            autentication.Name is null ? null : Hash.Create(HashCipherMode.SHA512).Update(autentication.Name).toBase64String()
        );

        if (user is null || !user.Active || !user.UserEnterprises.Where(a => a.Enterprise.EnterpriseId == autentication.EnterpriseId).Any()) return BadRequest(new EmptyResponseDto() { Message = "Credenciais inválidas" });
        var valid = Pbkdf2.Create(Pbkdf2Size._8192, Pbkdf2HashDerivation.HMACSHA512)
            .Verify(Binary.FromBase64(user.Password ?? string.Empty).Bytes, Binary.FromString(autentication.Password ?? string.Empty).Bytes);

        if (!valid) return BadRequest(new EmptyResponseDto() { Message = "Credenciais inválidas" });

        var code = this.authCodeService.Create(user);
// #if RELEASE
//         await this.SendTryLogin(user, code);
// #endif
        this.hostCache.Set("try:login", user, 480);
        this.hostCache.Set("try:login:enterprise", autentication.EnterpriseId, 480);

#if DEBUG
        return Ok(new EmptyResponseDto() { Message = code.Code });
#elif RELEASE 
        return Ok(new EmptyResponseDto() { Message = "Código de autenticação enviado" });
#endif
    }

    [HttpPost]
    public async Task<IActionResult> Enterprises(
        [FromBody] AuthenticationName authenticationName
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = this.service.FindByEmailOrName(
            authenticationName.Email,
            authenticationName.Name is null ? null : Hash.Create(HashCipherMode.SHA512).Update(authenticationName.Name).toBase64String()
        );

        return Ok(user?.UserEnterprises.Select(a => a.Enterprise) ?? new List<Enterprise>());
    }

    [HttpPost]
    public async Task<IActionResult> Code(
        [FromBody] AuthenticationCode authentication
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = this.hostCache.Get<User>("try:login");
        var enterpriseId = this.hostCache.Get<int>("try:login:enterprise");
        if (user is null)
            return BadRequest(
                new EmptyResponseDto() { 
                    Message = "Tentativa de login inválida"
                }
            );


        var code = this.authCodeService.Create(user);
        if (authentication.Code != code.Code)
            return BadRequest(
                new EmptyResponseDto()
                {
                    Message = "Código informado inválido"
                }
            );
        this.hostCache.Unset("try:login");
        this.authCodeService.Delete(user);

        return Ok(
            Jwt.Create(securityConfiguration)
                .Write(new ClaimIdentifier() { 
                    UserId = user.UserId,
                    Permissions = user.PermissionNames ?? new string[0],
                    Roles = user.RoleNames ?? new string[0],
                    EnterpriseId = enterpriseId
                })
        );
    }
}