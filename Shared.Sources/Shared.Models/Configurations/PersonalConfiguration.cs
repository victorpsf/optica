using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Models.Configurations;

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
        get => this.Manager.GetEnv("02117049fcb2465387f56e89e88f0bf67d9f97f56510f9c14e332cbd7dc773423981fc935616f7c3f6763fed08032053f6a4c21daccf4b0a2e481289f6730d0e").ToStringValue("");
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Financial", "Databases", "Postgres", "ConnectionString").ToStringValue("");
#endif
    }
}