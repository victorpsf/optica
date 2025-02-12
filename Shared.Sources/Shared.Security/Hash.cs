using System.Security.Cryptography;
using Shared.Libraries;
using Shared.Models.Security;

namespace Shared.Security;

public class Hash
{
    private HashCipherMode mode;
    
    public Hash(HashCipherMode mode)
        => this.mode = mode;
    
    public static Hash Create(HashCipherMode mode)
        => new(mode);

    public HashAlgorithm GetAlgorithm() => this.mode switch
    {
        HashCipherMode.SHA512 => SHA512.Create(),
        HashCipherMode.SHA384 => SHA384.Create(),
        HashCipherMode.SHA256 => SHA256.Create(),
        _ => throw new NotImplementedException(),
    };

    public byte[] Update(byte[] value)
        => this.GetAlgorithm().ComputeHash(value);

    public Binary Update(string value)
        => Binary.FromBytes(this.Update(Binary.FromString(value).Bytes));
}