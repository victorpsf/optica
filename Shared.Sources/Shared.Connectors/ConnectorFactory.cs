using Shared.Connectors.Interfaces;

namespace Shared.Connectors;

public class ConnectorFactory
{
    public static AuthenticationClient CreateAuthenticationClient(string baseUrl)
    {
        Console.WriteLine(baseUrl);
        return new AuthenticationClient(baseUrl);
    }
}
