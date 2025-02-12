using System.Security.Cryptography;
using Shared.Models.Security;

namespace Shared.Security;

public class RsaCryptography
{
    public AsymmetricCryptografyKeys Keys { get; set; }

    private RsaCryptography(AsymmetricCryptografyKeys keys)
    { this.Keys = keys; }

    private static int KeySize(RsaCryptographySize size) => size switch
    {
        RsaCryptographySize._1024 => 1024,
        RsaCryptographySize._2048 => 2048,
        RsaCryptographySize._4096 => 4096,
        RsaCryptographySize._8192 => 8192,
        _ => throw new NotImplementedException(),
    };
    
    public static AsymmetricCryptografyKeys GenerateKeys(RsaCryptographySize size)
    {
        using var rsa = RSA.Create(KeySize(size));

        return new()
        {
            PrivateKey = rsa.ExportPkcs8PrivateKey(),
            PublicKey = rsa.ExportSubjectPublicKeyInfo()
        };
    }

    public RsaCryptographyProvider getPublicProvider()
        => new(this.Keys.PublicKey, true);
    
    public RsaCryptographyProvider getPrivateProvider()
        => new(this.Keys.PrivateKey, false);

    public static RsaCryptography Create(AsymmetricCryptografyKeys keys)
        => new(keys);
    
    public static RsaCryptography Create(RsaCryptographySize size)
        => Create(
            GenerateKeys(size)
        );
    
    
}