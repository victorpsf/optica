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

    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string ConnectionString
    {
        get => this.Manager.GetSecret("Contexts", "Enterprise", "Databases", "Postgres", "ConnectionString").ToStringValue("");
    }
}