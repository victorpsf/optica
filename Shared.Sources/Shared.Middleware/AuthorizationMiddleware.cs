using Microsoft.AspNetCore.Authorization;
using Shared.Exceptions;
using Shared.Interfaces.Databases;
using Shared.Models;

namespace Shared.Middleware;

public class AuthorizationMiddleware: AuthorizationHandler<AuthorizationRequirements>
{
    private readonly AuthenticatedUser _authenticatedUser;
    private readonly IUserService _userService;
    
    public AuthorizationMiddleware (IUserService userService, AuthenticatedUser authenticatedUser) {
        this._authenticatedUser = authenticatedUser;
        this._userService = userService;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthorizationRequirements requirement
    ) {
        try
        {
            if (
                this._authenticatedUser.User is not null &&
                this._authenticatedUser.User.PermissionNames.Any(a => a.ToUpperInvariant() == requirement.Permission.ToUpperInvariant())
            ) context.Succeed(requirement);
            
            else throw new Exception("Unauthorized");
        }

        catch
        { context.Fail(); }
        
        return Task.CompletedTask;
    }
}