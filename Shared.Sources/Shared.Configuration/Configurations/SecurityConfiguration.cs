using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;
using Shared.Interfaces.Security;

namespace Shared.Configuration.Configurations;

public class SecurityConfiguration: ISecurityConfiguration
{
    public IManagerConfiguration Manager { get; private set; }
    public ISymmetricCryptography Crypto { get => this.Manager.Crypto; }

    public SecurityConfiguration(IManagerConfiguration manager)
        => this.Manager = manager;

    public static SecurityConfiguration GetConfiguration(IManagerConfiguration manager)
        => new(manager);
    
    public static SecurityConfiguration GetConfiguration(IConfiguration configuration)
        => GetConfiguration(ManagerConfiguration.GetInstance(configuration));

    public string TokenSecret
    {
#if RELEASE        
        get => this.Crypto.DecryptBase64(
                this.Manager.GetEnv(
                    "C71605A2F1BF4C6C39AC62F1E69618B8CC66D160C82F13E1FD05AEAD620A10E33DE014F506A01352A5C7053E435A59E3828B75C206E0C2379A0FC919EE986A80"
                ).ToStringValue("")
            ).toBinaryString();
#elif DEBUG
        get => this.Manager.GetSecret("Security", "Jwt", "Secret").ToStringValue("");
#endif
    }

    public string Issuer
    {
#if RELEASE
        get => this.Crypto.DecryptBase64(
                this.Manager.GetEnv(
                    "9F4E62FBD2B0A7EDFF0743EEBEE412DF5418A46DDD661F43B3D946103AC55A2EDF8FA72FC5020098910C1D752DBE9ADB07D5990C1802A383B7E9BA881D604620"
                ).ToStringValue("")
            ).toBinaryString();
#elif DEBUG
        get => this.Manager.GetSecret("Security", "Jwt", "Issuer").ToStringValue("");
#endif
    }

    public long TokenMinutes
    {
#if RELEASE
        get => ConfigurationProperty.Create(
                this.Crypto.DecryptBase64(
                    this.Manager.GetEnv(
                        "3936D1E11CD9D4AA6E39D4069A713E7BD56F65625BFCB8A83F090A582A35445B5067A929FA308E4DD5013864DC2F5F15717EC6CB5756DA588B8CD926E552BA2C"
                    ).ToStringValue("")
                ).toBinaryString()
            ).ToLongValue(480);
#elif DEBUG
        get => this.Manager.GetSecret("Security", "Jwt", "Minutes").ToLongValue(480);
#endif
    }
}