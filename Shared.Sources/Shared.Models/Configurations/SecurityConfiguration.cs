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

    public string Secret
    {
        #if RELEASE
        get => this.Manager.GetEnv("d8ffaab5f00aa940c1bff34ebe1ba959eccbeb5064a1b49d702727ce0c40878e82584ae601c0efa966b25dbe43e556ed6d6871121f89db3e903b53f90d607dc3").ToStringValue("");
        #elif DEBUG
        get => this.Manager.GetSecret("Security", "Symmetric", "Secret").ToStringValue("");
        #endif
    }
    
    public string TokenSecret
    {
#if RELEASE
        get => this.Manager.GetEnv("d8ffaab5f00aa940c1bff34ebe1ba959eccbeb5064a1b49d702727ce0c40878e82584ae601c0efa966b25dbe43e556ed6d6871121f89db3e903b53f90d607dc3").ToStringValue("");
#elif DEBUG
        get => this.Manager.GetSecret("Security", "Jwt", "Secret").ToStringValue("");
#endif
    }
    
    public string Issuer
    {
#if RELEASE
        get => this.Manager.GetEnv("d8ffaab5f00aa940c1bff34ebe1ba959eccbeb5064a1b49d702727ce0c40878e82584ae601c0efa966b25dbe43e556ed6d6871121f89db3e903b53f90d607dc3").ToStringValue("");
#elif DEBUG
        get => this.Manager.GetSecret("Security", "Jwt", "Issuer").ToStringValue("");
#endif
    }

    public long TokenMinutes
    {
#if RELEASE
        get => this.Manager.GetEnv("d8ffaab5f00aa940c1bff34ebe1ba959eccbeb5064a1b49d702727ce0c40878e82584ae601c0efa966b25dbe43e556ed6d6871121f89db3e903b53f90d607dc3").ToStringValue("");
#elif DEBUG
        get => this.Manager.GetSecret("Security", "Jwt", "Minutes").ToLongValue(480);
#endif
    }
}