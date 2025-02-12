using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Models.Configurations;

public class EnterpriseConfiguration: ModuleBaseConfiguration, IEnterpriseConfiguration
{
    public EnterpriseConfiguration(
        IManagerConfiguration manager,
        ISecurityConfiguration security
    ): base(manager, security) { }

    public static EnterpriseConfiguration GetConfiguration(IConfiguration configuration)
    {
        var manager = ManagerConfiguration.GetInstance(configuration);
        var security = SecurityConfiguration.GetInstance(configuration);
        
        return new EnterpriseConfiguration(
            manager, 
            security
        );
    }

    public string ConnectionString
    {
#if RELEASE
        get => this.Manager.GetEnv("5db78615382f46570766c68912fbf3ff3b693018fe3a2c54c3848551a3d7e06bcc896b56ec79d04bc2f71b4e77c4854f67742551d2f49f8e749366bb54d89259").ToStringValue("");
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Enterprise", "Databases", "Postgres", "ConnectionString").ToStringValue("");
#endif
    }
}