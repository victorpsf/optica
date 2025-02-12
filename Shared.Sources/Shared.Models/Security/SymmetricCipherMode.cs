namespace Shared.Models.Security;

public enum SymmetricCipherMode
{
    AES_128_CBC,
    AES_192_CBC,
    AES_256_CBC,
    
    AES_128_ECB,
    AES_192_ECB,
    AES_256_ECB,
    
    AES_128_OFB,
    AES_192_OFB,
    AES_256_OFB,
    
    AES_128_CFB,
    AES_192_CFB,
    AES_256_CFB,
}