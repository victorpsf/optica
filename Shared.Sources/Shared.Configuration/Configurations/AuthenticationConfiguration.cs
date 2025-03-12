using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Configuration.Configurations;

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
        get => this.Manager.Crypto.DecryptBase64(
                this.Manager.GetEnv(
                    "787B2FE41A1D28BD226D673C05E8B313F7267BEDE57D97E1427E5497A370E4035E5707E0A72C7E8441E2DD0ED0A550CA04A307E016DBC48E9E4D6CCFAEF50982"
                ).ToStringValue("")
            ).toBinaryString();
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Authentication", "Databases", "Postgres", "ConnectionString").ToStringValue("");
#endif
    }
}