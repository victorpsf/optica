namespace Shared.Interfaces.Configurations;

public interface IAuthenticationConfiguration: IModuleBaseConfiguration
{
    string ConnectionString { get; }
}