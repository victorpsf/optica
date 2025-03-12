namespace Shared.Interfaces.Configurations;

public interface ISecurityConfiguration
{
    IManagerConfiguration Manager { get; }
    
    string Issuer { get; }
    string TokenSecret { get; }
    long TokenMinutes { get; }
}