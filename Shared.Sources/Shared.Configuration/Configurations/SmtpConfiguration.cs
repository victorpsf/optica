using Microsoft.Extensions.Configuration;
using Shared.Interfaces.Configurations;

namespace Shared.Configuration.Configurations;

public class SmtpConfiguration: ModuleBaseConfiguration, ISmtpConfiguration
{
    public SmtpConfiguration(
        IManagerConfiguration manager,
        ISecurityConfiguration security
    ): base(manager, security) { }

    public static SmtpConfiguration GetConfiguration(IConfiguration configuration)
    {
        var manager = ManagerConfiguration.GetInstance(configuration);
        var security = SecurityConfiguration.GetInstance(configuration);
        
        return new SmtpConfiguration(
            manager, 
            security
        );
    }

    public string Username
    {
#if RELEASE
        get => this.Manager.Crypto.DecryptBase64(
            this.Manager.GetEnv(
                "70CB56112F16125DA9DDC9C877512CDE975A19C5D58577F3BD6783B3C6B75F188911AC6145F5310C40BECE886B0FE06A2E24827BDE824BD1372A1190F1F4736F"
            ).ToStringValue("")
        ).toBinaryString();
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "User").ToStringValue("");
#endif
    }
    
    public string Password
    {
#if RELEASE
        get => this.Manager.Crypto.DecryptBase64(
            this.Manager.GetEnv(
                "D865B08EE7996FEE52C4CACE5EE56DC122231E74FACE4C356AC7925085884D1C52DDF264ABCA6A40CF4C060D7CD3A3E9F50F8F966096875292821D4841E36109"
            ).ToStringValue("")
        ).toBinaryString();
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "Password").ToStringValue("");
#endif
    }
    
    public string Host
    {
#if RELEASE
        get => this.Manager.Crypto.DecryptBase64(
            this.Manager.GetEnv(
                "EFE55B78A34B58CA6E4EBA4EF2424785768D4FEF5537B44D6E2252E12D99D4D817CCE75344BC78014993629149488CFCB107C27D5FF5C4A88855EFA9EE5A988D"
            ).ToStringValue("")
        ).toBinaryString();
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "Host").ToStringValue("");
#endif
    }
    
    public int Port
    {
#if RELEASE
        get => ConfigurationProperty.Create(
            this.Manager.Crypto.DecryptBase64(
                this.Manager.GetEnv(
                    "C5B98B9A4AC609CF4291A86B232868E31914B362EF9400B08541805A495B80FF18F43ED3D369727AB59CD028B84B7CC592CA579C4318D427256BBD44B4F178D3"
                ).ToStringValue("")
            ).toBinaryString()).ToIntValue(443);
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "Port").ToIntValue(443);
#endif
    }

    public bool UseSsl
    {
#if RELEASE
        get => ConfigurationProperty.Create(
            this.Manager.Crypto.DecryptBase64(
                this.Manager.GetEnv(
                    "4DCC1737E15011CBC64DFF43498AE420AB1DB9D1E9DFA7AAFC83D22B8D4F0258C8936707EF1000F2D4AA4397FFAA43EA497B02A078523DFB3E9B6EB2D367C7DC"
                ).ToStringValue("")
            ).toBinaryString()
        ).toBoolValue(true);
#elif DEBUG
        get => this.Manager.GetSecret("Contexts", "Services", "Smtp", "Ssl").toBoolValue(true);
#endif
    }
}