namespace Shared.Interfaces.Configurations;

public interface ISmtpConfiguration: IModuleBaseConfiguration
{
    IManagerConfiguration Manager { get; }
    ISecurityConfiguration Security { get; }
    
    string Username { get; }
    string Password { get; }
    string Host { get; }
    int Port { get; }
    bool UseSsl { get; }
}