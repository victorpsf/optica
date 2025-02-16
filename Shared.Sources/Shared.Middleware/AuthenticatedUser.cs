using Microsoft.AspNetCore.Http;
using Shared.Databases.Entities;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Interfaces.Configurations;
using Shared.Interfaces.Databases;
using Shared.Models.Security;
using Shared.Security;

namespace Shared.Middleware;

public class AuthenticatedUser
{
    private readonly HttpContext? HttpContext;
    private ClaimIdentifier? claim;
    private ISecurityConfiguration SecurityConfiguration;
    private IAuthenticationService AuthenticationService;
    private User? user;
    
    private string? Token
    {
        get
        {
            var token = this.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last<string>();
            
            if (token.IsNullOrEmpty())
                throw new NotAuthoriazedException();
            
            return token;
        } 
    }
    
    public ClaimIdentifier? Claim
    {
        get
        {
            if (this.HttpContext is null || this.Token.IsNullOrEmpty())
                throw new NotAuthoriazedException();

            if (this.claim is null)
                this.claim = Jwt.Create(this.SecurityConfiguration).Read(this.Token ?? string.Empty);

            return this.claim;
        }
    }

    public User? User
    {
        get
        {
            if (this.user is not null)
                return this.user;
            
            if (this.Claim is null)
                throw new NotAuthoriazedException();
            
            this.user = this.AuthenticationService.FindById(this.Claim.UserId);
            return this.user;
        }
    }

    public AuthenticatedUser(
        IHttpContextAccessor httpContextAcessor,
        ISecurityConfiguration securityConfiguration,
        IAuthenticationService authenticationService
    )
    {
        this.HttpContext = httpContextAcessor.HttpContext;
        this.SecurityConfiguration = securityConfiguration;
        this.AuthenticationService = authenticationService;
    }
}