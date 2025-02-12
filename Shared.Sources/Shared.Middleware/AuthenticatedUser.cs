using Microsoft.AspNetCore.Http;
using Shared.Interfaces.Configurations;
using Shared.Models.Security;
using Shared.Security;

namespace Shared.Middleware;

public class AuthenticatedUser
{
    private readonly HttpContext? HttpContext;
    private ClaimIdentifier? claim;
    private ISecurityConfiguration SecurityConfiguration;
    
    private void HandleException() => throw new UnauthorizedAccessException("Unautorized");

    private string Token
    { get => this.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last<string>() ?? string.Empty; }
    
    public ClaimIdentifier Claim
    {
        get
        {
            if (this.HttpContext is null) 
                this.HandleException();

            if (this.claim is null)
                this.claim = Jwt.Create(this.SecurityConfiguration).Read(this.Token);

            return this.claim ?? new();
        }
    }

    public AuthenticatedUser(
        IHttpContextAccessor httpContextAcessor,
        ISecurityConfiguration securityConfiguration
    )
    {
        this.HttpContext = httpContextAcessor.HttpContext;
        this.SecurityConfiguration = securityConfiguration;
    }
}