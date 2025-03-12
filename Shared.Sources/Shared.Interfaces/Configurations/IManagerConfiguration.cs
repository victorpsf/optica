using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Security;

namespace Shared.Interfaces.Configurations;

public interface IManagerConfiguration
{
    IConfiguration Configuration { get; }
    ISymmetricCryptography Crypto { get; }
    string Secret { get; }

    IConfigurationProperty GetEnv(string name);
    IConfigurationProperty GetSecret(params string[] names);
}