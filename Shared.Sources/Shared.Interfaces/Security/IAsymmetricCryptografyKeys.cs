
namespace Shared.Interfaces.Security;

public interface IAsymmetricCryptografyKeys
{
    byte[] PrivateKey { get; }
    byte[] PublicKey { get; }
}
