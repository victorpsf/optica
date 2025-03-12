using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Configuration.Configurations;

public class PersonalConfiguration: ModuleBaseConfiguration, IPersonalConfiguration
{
    public PersonalConfiguration(
        IManagerConfiguration manager,
        ISecurityConfiguration security
    ): base(manager, security) { }

    public static PersonalConfiguration GetConfiguration(IConfiguration configuration)
    {
        var manager = ManagerConfiguration.GetInstance(configuration);
        var security = SecurityConfiguration.GetInstance(configuration);
        
        return new PersonalConfiguration(
            manager, 
            security
        );
    }

    public string ConnectionString
    {
#if RELEASE
        get => this.Manager.Crypto.DecryptBase64(
                this.Manager.GetEnv(
                    "e718b667d4af3702f5d6b7c19239947231bf4408068d276b7c448ad1ee680e7f79ce5fa41db3608273e5e29c3ed3578659431ff00c3161d089d6e56a5616eb1d"
                ).ToStringValue("")
            ).toBinaryString();
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Personal", "Databases", "Postgres", "ConnectionString").ToStringValue("");
#endif
    }
}