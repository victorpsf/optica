
using System.Security.Cryptography;

namespace Shared.Interfaces.Security;

public interface IRsaCryptographyProvider
{
    byte[] Key { get; }
    byte[] Encrypt(byte[] data);
    byte[] Decrypt(byte[] data);
}
