namespace Shared.Interfaces.Security;

public interface IAuthToken
{
    string Token { get; }
    string Type { get;  }
}
