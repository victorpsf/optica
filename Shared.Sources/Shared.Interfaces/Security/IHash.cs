using System.Security.Cryptography;

namespace Shared.Interfaces.Security;

public interface IHash
{
    byte[] Update(byte[] value);
    IBinary Update(string value);
}
