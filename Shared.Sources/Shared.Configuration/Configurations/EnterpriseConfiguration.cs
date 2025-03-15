using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Configuration.Configurations;

public class EnterpriseConfiguration: ModuleBaseConfiguration, IEnterpriseConfiguration
{
    public EnterpriseConfiguration(
        IManagerConfiguration manager
    ): base(manager) { }

    public static EnterpriseConfiguration GetConfiguration(IConfiguration configuration)
        => new(ManagerConfiguration.GetInstance(configuration));

    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string ConnectionString
    {
        get => this.Manager.GetSecret("Contexts", "Enterprise", "Databases", "Postgres", "ConnectionString").ToStringValue("");
    }
}