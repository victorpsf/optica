using System.Security.Cryptography;

namespace Shared.Security;

public class RsaCryptographyProvider
{
    public byte[] Key { get; set; }
    private bool isPublic;

    public RsaCryptographyProvider(byte[] key, bool isPublic)
    {
        this.Key = key;;
        this.isPublic = isPublic;
    }

    private RSA getProvider()
    {
        var rsa = RSA.Create();
        if (this.isPublic)
            rsa.ImportSubjectPublicKeyInfo(this.Key, out _);
        else
            rsa.ImportPkcs8PrivateKey(this.Key, out _);

        return rsa;
    }

    public byte[] Encrypt(byte[] data)
    {
        using var rsa = this.getProvider();
        return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
    }

    public byte[] Decrypt(byte[] data)
    {
        if (this.isPublic)
            throw new OperationCanceledException("Cannot decrypt data because it is public key");

        using var rsa = this.getProvider();
        return rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
    }
}