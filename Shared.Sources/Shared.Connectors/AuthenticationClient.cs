using Shared.Connectors.Interfaces;
using Shared.Dtos.Input.AuthenticationModules;

namespace Shared.Connectors;

public class AuthenticationClient: IAuthenticationConnector<object>
{
    private HttpClient client;

    public AuthenticationClient(string baseUrl)
    {
        this.client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public async Task<object> Index(
        Autentication autentication
    ) {
        return new { };
    }

    public async Task<object> Code(
        AuthenticationCode authentication
    ) {
        return new { };
    }
}
