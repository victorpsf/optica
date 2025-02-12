using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Models.Configurations;

public class ManagerConfiguration: IManagerConfiguration
{
    public IConfiguration Configuration { get; private set; }

    public ManagerConfiguration(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }
    
    public static IManagerConfiguration GetInstance(IConfiguration configuration) => new ManagerConfiguration(configuration);

    public string ToJsonPath(string[] parts)
    {
        #if RELEASE
        return string.Concat("_", parts);
        #elif DEBUG
        return string.Join(":", parts);
        #endif
    }
    
    public IConfigurationProperty GetEnv(string name)
        => new ConfigurationProperty() { Value = Environment.GetEnvironmentVariable(name) };

    public IConfigurationProperty GetSecret(params string[] names) 
        => new ConfigurationProperty() { Value = this.Configuration.GetSection(this.ToJsonPath(names)).Value };
}