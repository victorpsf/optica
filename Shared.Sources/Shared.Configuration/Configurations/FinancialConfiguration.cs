using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Configuration.Configurations;

public class FinancialConfiguration: ModuleBaseConfiguration, IFinancialConfiguration
{
    public FinancialConfiguration(
        IManagerConfiguration manager
    ): base(manager) { }

    public static FinancialConfiguration GetConfiguration(IConfiguration configuration)
        => new (
            ManagerConfiguration.GetInstance(configuration)
        );

    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string ConnectionString
    {
        get => this.Manager.GetSecret("Contexts", "Financial", "Databases", "Postgres", "ConnectionString").ToStringValue("");
    }
}