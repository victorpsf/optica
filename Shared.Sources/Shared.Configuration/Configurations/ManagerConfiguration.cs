using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;
using Shared.Interfaces.Security;
using Shared.Models.Security;
using Shared.Security;

namespace Shared.Configuration.Configurations;

public class ManagerConfiguration: IManagerConfiguration
{
    public IConfiguration Configuration { get; private set; }

    public ManagerConfiguration(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }
    
    public static IManagerConfiguration GetInstance(IConfiguration configuration) => new ManagerConfiguration(configuration);

    private string ToJsonPath(string[] parts) => string.Join(":", parts);

    public IConfigurationProperty GetEnv(string name)
        => new ConfigurationProperty() { Value = Environment.GetEnvironmentVariable(name) };

    public IConfigurationProperty GetSecret(params string[] names) 
        => new ConfigurationProperty() { Value = this.Configuration.GetSection(this.ToJsonPath(names)).Value };

    public string Secret
    {
#if RELEASE
        get => this.GetEnv("11345E55DFA459206A13E4DB74ADE366FACBF87B8DCD22DFA1CC8E3CF92F52FEC3DA520106FD868E8F499EC403FA6E5B6366ED9ECEEA9C80A54AC7FDDAB9A1F2").ToStringValue("");
#elif DEBUG
        get => this.GetSecret("Security", "Symmetric", "Secret").ToStringValue("");
#endif
    }

    public ISymmetricCryptography Crypto
    {
        get => SymmetricCryptography.Create(SymmetricCipherMode.AES_256_CBC).SetPassword(this.Secret);
    }
}