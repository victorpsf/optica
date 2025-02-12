using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Models.Configurations;

public class AuthenticationConfiguration: ModuleBaseConfiguration, IAuthenticationConfiguration
{
    public AuthenticationConfiguration(
        IConfiguration configuration
    ): base(ManagerConfiguration.GetInstance(configuration), SecurityConfiguration.GetInstance(configuration)) { }

    public static AuthenticationConfiguration GetConfiguration(IConfiguration configuration)
        => new(configuration);

    public string ConnectionString
    {
#if RELEASE
        get => this.Manager.GetEnv("e718b667d4af3702f5d6b7c19239947231bf4408068d276b7c448ad1ee680e7f79ce5fa41db3608273e5e29c3ed3578659431ff00c3161d089d6e56a5616eb1d").ToStringValue("");
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Authentication", "Databases", "Postgres", "ConnectionString").ToStringValue("");
#endif
    }
}