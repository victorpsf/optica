using Microsoft.AspNetCore.Authorization;
using Shared.Exceptions;
using Shared.Interfaces.Databases;
using Shared.Models;

namespace Shared.Middleware;

public class AuthorizationMiddleware: AuthorizationHandler<AuthorizationRequirements>
{
    private readonly AuthenticatedUser _authenticatedUser;
    private readonly IAuthenticationService authenticationService;
    
    public AuthorizationMiddleware (IAuthenticationService authenticationService, AuthenticatedUser authenticatedUser) {
        this._authenticatedUser = authenticatedUser;
        this.authenticationService = authenticationService;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthorizationRequirements requirement
    ) {
        try
        {
            // [TODO]: ADICIONAR VERIFICAÇÃO DE PERMISSÕES DO USUÁRIO
            context.Succeed(requirement);
        }

        catch (Exception e)
        {
            context.Fail();
        }
        
        return Task.CompletedTask;
    }
}