namespace Shared.Interfaces.Configurations;

public interface IEnterpriseConfiguration: IModuleBaseConfiguration
{
    IManagerConfiguration Manager { get; }
    ISecurityConfiguration Security { get; }
    string ConnectionString { get; }
}