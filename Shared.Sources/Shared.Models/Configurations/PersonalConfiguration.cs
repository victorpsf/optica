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

    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string ConnectionString
    {
        get => this.Manager.GetSecret("Contexts", "Financial", "Databases", "Postgres", "ConnectionString").ToStringValue("");
    }
}