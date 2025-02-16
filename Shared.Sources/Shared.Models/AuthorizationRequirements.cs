using Microsoft.AspNetCore.Authorization;

namespace Shared.Models;

public class AuthorizationRequirements: IAuthorizationRequirement
{
    public string Permission { get; private set; }

    public AuthorizationRequirements(string permission)
    { this.Permission = permission; }
}