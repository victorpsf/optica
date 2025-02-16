using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Models.Configurations;

public class SecurityConfiguration: ISecurityConfiguration
{
    public IManagerConfiguration Manager { get; private set; }

    public SecurityConfiguration(IManagerConfiguration manager)
        => this.Manager = manager;

    public static ISecurityConfiguration GetInstance(IConfiguration configuration)
        => new SecurityConfiguration(ManagerConfiguration.GetInstance(configuration));
    
    public static ISecurityConfiguration GetInstance(IManagerConfiguration manager)
        => new SecurityConfiguration(manager);

    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string Secret
    {
        get => this.Manager.GetSecret("Security", "Symmetric", "Secret").ToStringValue("");
    }
    
    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string TokenSecret
    {
        get => this.Manager.GetSecret("Security", "Jwt", "Secret").ToStringValue("");
    }
    
    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public string Issuer
    {
        get => this.Manager.GetSecret("Security", "Jwt", "Issuer").ToStringValue("");
    }

    // [TODO]: ADICIONAR VARIAVEL DE AMBIENTE
    public long TokenMinutes
    {
        get => this.Manager.GetSecret("Security", "Jwt", "Minutes").ToLongValue(480);
    }
}