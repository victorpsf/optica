using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Models.Configurations;

public class SmtpConfiguration: ModuleBaseConfiguration, ISmtpConfiguration
{
    public SmtpConfiguration(
        IManagerConfiguration manager,
        ISecurityConfiguration security
    ): base(manager, security) { }

    public static SmtpConfiguration GetConfiguration(IConfiguration configuration)
    {
        var manager = ManagerConfiguration.GetInstance(configuration);
        var security = SecurityConfiguration.GetInstance(configuration);
        
        return new SmtpConfiguration(
            manager, 
            security
        );
    }

    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string Username
    {
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "User").ToStringValue("");
    }
    
    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string Password
    {
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "Password").ToStringValue("");
    }
    
    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string Host
    {
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "Host").ToStringValue("");
    }
    
    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public int Port
    {
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "Port").ToIntValue(443);
    }
    
    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public bool UseSsl
    {
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "Ssl").toBoolValue(true);
    }
}