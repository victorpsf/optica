using Shared.Interfaces.Security;

namespace Shared.Models.Security;

public class AuthToken: IAuthToken
{
    public string Token { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}