using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Models.Configurations;

public class ModuleBaseConfiguration: IModuleBaseConfiguration
{
    public IManagerConfiguration Manager { get; set; }
    public ISecurityConfiguration Security { get; private set; }

    public ModuleBaseConfiguration(
        IManagerConfiguration manager,
        ISecurityConfiguration security
    )
    {
        this.Manager = manager;
        this.Security = security;
    }
}