namespace Shared.Interfaces.Configurations;

public interface IPersonalConfiguration: IModuleBaseConfiguration
{
    IManagerConfiguration Manager { get; }
    ISecurityConfiguration Security { get; }
    string ConnectionString { get; }
}