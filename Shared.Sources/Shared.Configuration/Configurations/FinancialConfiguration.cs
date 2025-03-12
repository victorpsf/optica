using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Configuration.Configurations;

public class FinancialConfiguration: ModuleBaseConfiguration, IFinancialConfiguration
{
    public FinancialConfiguration(
        IManagerConfiguration manager,
        ISecurityConfiguration security
    ): base(manager, security) { }

    public static FinancialConfiguration GetConfiguration(IConfiguration configuration)
    {
        var manager = ManagerConfiguration.GetInstance(configuration);
        var security = SecurityConfiguration.GetInstance(configuration);
        
        return new FinancialConfiguration(
            manager, 
            security
        );
    }

    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string ConnectionString
    {
        get => this.Manager.GetSecret("Contexts", "Financial", "Databases", "Postgres", "ConnectionString").ToStringValue("");
    }
}