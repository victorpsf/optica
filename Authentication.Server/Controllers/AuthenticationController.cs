using Authentication.Server.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces.Configurations;
using Shared.Interfaces.Databases;
using Shared.Libraries;
using Shared.Models.Security;
using Shared.Security;

namespace Authentication.Server;

[AllowAnonymous]
public class AuthenticationController: ControllerBase
{
    private ISecurityConfiguration securityConfiguration;
    private IAuthenticationService service;

    public AuthenticationController(
        ISecurityConfiguration securityConfiguration,
        IAuthenticationService service
    ) {
        this.service = service;
        this.securityConfiguration = securityConfiguration;
    }

    [HttpPost]
    public IActionResult Index(
        [FromBody] Autentication autentication
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = this.service.FindByEmailOrName(
            autentication.Email,
            autentication.Name is null ? null : Hash.Create(HashCipherMode.SHA512).Update(autentication.Name).toBase64String()
        );
        
        if (user is null || !user.Active)
            return BadRequest();

        var valid = Pbkdf2.Create(Pbkdf2Size._8192, Pbkdf2HashDerivation.HMACSHA512)
            .Verify(Binary.FromBase64(user.Password ?? string.Empty).Bytes, Binary.FromString(autentication.Password ?? string.Empty).Bytes);
        
        if (!valid)
            return BadRequest();
        
        return Ok(
            Jwt.Create(securityConfiguration).Write(new ClaimIdentifier()
            {
                UserId = user.Id
            })
        );
    }
}