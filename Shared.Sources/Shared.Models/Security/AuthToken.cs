namespace Shared.Models.Security;

public class AuthToken
{
    public string Token { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}