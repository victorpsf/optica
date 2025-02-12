using Microsoft.Extensions.Configuration;

namespace Shared.Interfaces.Configurations;

public interface IManagerConfiguration
{
    IConfiguration Configuration { get; }

    IConfigurationProperty GetEnv(string name);
    IConfigurationProperty GetSecret(params string[] names);
}