using System.Security.Cryptography;
using Shared.Interfaces.Security;
using Shared.Models.Security;

namespace Shared.Security;

public class RsaCryptography: IRsaCryptography
{
    public IAsymmetricCryptografyKeys Keys { get; set; }

    private RsaCryptography(IAsymmetricCryptografyKeys keys)
    { this.Keys = keys; }

    private static int KeySize(RsaCryptographySize size) => size switch
    {
        RsaCryptographySize._1024 => 1024,
        RsaCryptographySize._2048 => 2048,
        RsaCryptographySize._4096 => 4096,
        RsaCryptographySize._8192 => 8192,
        _ => throw new NotImplementedException(),
    };
    
    public static IAsymmetricCryptografyKeys GenerateKeys(RsaCryptographySize size)
    {
        using var rsa = RSA.Create(KeySize(size));

        return new AsymmetricCryptografyKeys()
        {
            PrivateKey = rsa.ExportPkcs8PrivateKey(),
            PublicKey = rsa.ExportSubjectPublicKeyInfo()
        };
    }

    public IRsaCryptographyProvider getPublicProvider()
        => new RsaCryptographyProvider(this.Keys.PublicKey, true);
    
    public IRsaCryptographyProvider getPrivateProvider()
        => new RsaCryptographyProvider(this.Keys.PrivateKey, false);

    public static IRsaCryptography Create(IAsymmetricCryptografyKeys keys)
        => new RsaCryptography(keys);
    
    public static IRsaCryptography Create(RsaCryptographySize size)
        => Create(
            GenerateKeys(size)
        );
}