using Shared.Interfaces.Security;

namespace Shared.Models.Security;

public class AsymmetricCryptografyKeys: IAsymmetricCryptografyKeys
{
    public byte[] PrivateKey { get; set; }
    public byte[] PublicKey { get; set; }
}