

namespace Shared.Interfaces.Security;

public interface IRsaCryptography
{
    IAsymmetricCryptografyKeys Keys { get; }
    IRsaCryptographyProvider getPublicProvider();
    IRsaCryptographyProvider getPrivateProvider();
}
