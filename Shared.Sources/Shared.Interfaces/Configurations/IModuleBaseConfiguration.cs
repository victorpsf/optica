namespace Shared.Interfaces.Configurations;

public interface IModuleBaseConfiguration
{
    IManagerConfiguration Manager { get; }
    ISecurityConfiguration Security { get; }
}