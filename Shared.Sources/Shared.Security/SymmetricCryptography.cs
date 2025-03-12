using System.Security.Cryptography;
using Shared.Libraries;
using Shared.Models.Security;
using Shared.Extensions;
using Shared.Security.Managers;
using Shared.Interfaces.Security;
using Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Shared.Security;

public class SymmetricCryptography: ISymmetricCryptography
{
    private SymmetricCipherMode mode;
    private byte[] key = new byte[0];

    public SymmetricCryptography(SymmetricCipherMode mode)
        => this.mode = mode;

    public SymmetricInfo GetInfo() => this.mode switch
    {
        SymmetricCipherMode.AES_128_CBC => new() { KeySize = 128, BlockSize = 128 },
        SymmetricCipherMode.AES_192_CBC => new() { KeySize = 192, BlockSize = 128 },
        SymmetricCipherMode.AES_256_CBC => new() { KeySize = 256, BlockSize = 128 },
        SymmetricCipherMode.AES_128_ECB => new() { KeySize = 128, BlockSize = 128 },
        SymmetricCipherMode.AES_192_ECB => new() { KeySize = 192, BlockSize = 128 },
        SymmetricCipherMode.AES_256_ECB => new() { KeySize = 256, BlockSize = 128 },
        SymmetricCipherMode.AES_128_OFB => new() { KeySize = 128, BlockSize = 128 },
        SymmetricCipherMode.AES_192_OFB => new() { KeySize = 192, BlockSize = 128 },
        SymmetricCipherMode.AES_256_OFB => new() { KeySize = 256, BlockSize = 128 },
        SymmetricCipherMode.AES_128_CFB => new() { KeySize = 128, BlockSize = 128 },
        SymmetricCipherMode.AES_192_CFB => new() { KeySize = 192, BlockSize = 128 },
        SymmetricCipherMode.AES_256_CFB => new() { KeySize = 256, BlockSize = 128 },

        _ => throw new NotImplementedException()
    };

    public CipherMode GetCipher()
    {
        return this.mode switch
        {
            SymmetricCipherMode.AES_128_CBC or SymmetricCipherMode.AES_192_CBC or SymmetricCipherMode.AES_256_CBC => CipherMode.CBC,
            SymmetricCipherMode.AES_128_ECB or SymmetricCipherMode.AES_192_ECB or SymmetricCipherMode.AES_256_ECB => CipherMode.ECB,
            SymmetricCipherMode.AES_128_OFB or SymmetricCipherMode.AES_192_OFB or SymmetricCipherMode.AES_256_OFB => CipherMode.CFB,
            SymmetricCipherMode.AES_128_CFB or SymmetricCipherMode.AES_192_CFB or SymmetricCipherMode.AES_256_CFB => CipherMode.CFB,
            _ => throw new NotImplementedException(),
        };
    }

    public PaddingMode GetPadding()
    {
        return this.mode switch
        {
            SymmetricCipherMode.AES_128_CBC or SymmetricCipherMode.AES_192_CBC or SymmetricCipherMode.AES_256_CBC => PaddingMode.None,
            SymmetricCipherMode.AES_128_ECB or SymmetricCipherMode.AES_192_ECB or SymmetricCipherMode.AES_256_ECB => PaddingMode.None,
            SymmetricCipherMode.AES_128_OFB or SymmetricCipherMode.AES_192_OFB or SymmetricCipherMode.AES_256_OFB => PaddingMode.None,
            SymmetricCipherMode.AES_128_CFB or SymmetricCipherMode.AES_192_CFB or SymmetricCipherMode.AES_256_CFB => PaddingMode.None,
            _ => PaddingMode.None,
        };
    }

    public static SymmetricCryptography Create(SymmetricCipherMode cipher)
        => new(cipher);

    public ISymmetricCryptography SetPassword(string password)
    {
        this.key = Binary.FromString(password).Bytes;
        return this;
    }

    public void GetProvider(out Aes provider, out SymmetricInfo info)
    {
        info = this.GetInfo();
        provider = Aes.Create();

        provider.BlockSize = info.BlockSize;
        provider.KeySize = info.KeySize;
        provider.Mode = this.GetCipher();
        provider.Padding = this.GetPadding();
    }

    public byte[] RandomBytes(int size)
    {
        byte[] value = new byte[size];
        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(value);
        return value;
    }

    public byte[] AppendEmptyBytes(byte[] value, int size)
    {
        byte[] bytes = new byte[size];

        for (int i = 0; i < size; i++)
            if (i < value.Length) bytes[i] = value[i];
            else break;

        return bytes;
    }

    public byte[] GenerateKeyBytes(byte[] value, int size)
    {
        var key = Hash.Create(HashCipherMode.SHA512).Update(value);
        return AppendEmptyBytes(key, size);
    }

    private byte[] Encrypt(
        byte[] buffer,
        byte[] key,
        byte[] iv,
        Aes provider
    )
    {
        provider.Key = key;
        provider.IV = iv;

        using (var encryptor = provider.CreateEncryptor(provider.Key, provider.IV))
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                cs.Write(buffer, 0, buffer.Length);
            return ms.ToArray();
        }
    }

    public byte[] Encrypt(byte[] buffer)
    {
        this.GetProvider(
            out Aes provider,
            out SymmetricInfo info
        );

        var key = this.GenerateKeyBytes(this.key, info.KeySize / 8);
        var iv = this.RandomBytes(info.BlockSize / 8);
        var encrypted = new List<byte>();

        foreach (var block in buffer.GetPartsBySouce(provider.BlockSize).Select(a => this.AppendEmptyBytes(a, provider.BlockSize)))
        {
            var a = this.Encrypt(block.ToArray(), key, iv, provider);
            encrypted.AddRange(a);
        }

        return SymmetricManager.Write(iv, encrypted.ToArray()).Bytes;
    }

    public IBinary EncryptBase64(string base64String)
        => Binary.FromBytes(this.Encrypt(Binary.FromBase64(base64String).Bytes));

    public IBinary EncryptHex(string hexString)
        => Binary.FromBytes(this.Encrypt(Binary.FromHex(hexString).Bytes));

    public IBinary EncryptString(string value)
        => Binary.FromBytes(this.Encrypt(Binary.FromString(value).Bytes));

    private byte[] Decrypt(
        byte[] buffer,
        byte[] key,
        byte[] iv,
        Aes provider
    )
    {
        provider.Key = key;
        provider.IV = iv;

        ICryptoTransform decryptor = provider.CreateDecryptor(provider.Key, provider.IV);
        using (var ms = new MemoryStream(buffer))
        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
        using (var sr = new StreamReader(cs))
            return Binary.FromString(sr.ReadToEnd()).Bytes;
    }

    public byte[] Decrypt(byte[] value)
    {
        this.GetProvider(
            out Aes provider,
            out SymmetricInfo info
        );

        var key = this.GenerateKeyBytes(this.key, provider.KeySize / 8);
        var readed = SymmetricManager.Read(value, provider.BlockSize / 8);
        var decrypted = new List<byte>();

        foreach (var block in readed.value.GetPartsBySouce(provider.BlockSize))
            decrypted.AddRange(this.Decrypt(block.ToArray(), key, readed.iv, provider));

        return decrypted.ToArray().Where(a => a != 0).ToArray();
    }
    
    public IBinary DecryptBase64(string base64String)
        => Binary.FromBytes(this.Decrypt(Binary.FromBase64(base64String).Bytes));
    
    public IBinary DecryptHex(string hexString)
        => Binary.FromBytes(this.Decrypt(Binary.FromHex(hexString).Bytes));
    
    public IBinary DecryptString(string value)
        => Binary.FromBytes(this.Decrypt(Binary.FromString(value).Bytes));
}
