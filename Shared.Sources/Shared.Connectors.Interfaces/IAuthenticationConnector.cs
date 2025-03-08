using Shared.Dtos.Input.AuthenticationModules;

namespace Shared.Connectors.Interfaces;

public interface IAuthenticationConnector<T>
{
    Task<T> Index(
        Autentication autentication
    );

    Task<T> Code(
        AuthenticationCode authentication
    );
}
