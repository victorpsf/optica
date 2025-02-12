namespace Shared.Interfaces.Configurations;

public interface IFinancialConfiguration: IModuleBaseConfiguration
{
    IManagerConfiguration Manager { get; }
    ISecurityConfiguration Security { get; }
    string ConnectionString { get; }
}