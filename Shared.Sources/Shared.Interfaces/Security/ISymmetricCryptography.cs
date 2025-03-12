namespace Shared.Interfaces.Security;

public interface ISymmetricCryptography
{

    ISymmetricCryptography SetPassword(string password);
    byte[] RandomBytes(int size);
    byte[] AppendEmptyBytes(byte[] value, int size);
    byte[] GenerateKeyBytes(byte[] value, int size);

    byte[] Encrypt(byte[] buffer);
    IBinary EncryptBase64(string base64String);
    IBinary EncryptHex(string hexString);
    IBinary EncryptString(string value);

    byte[] Decrypt(byte[] value);
    IBinary DecryptBase64(string base64String);
    IBinary DecryptHex(string hexString);
    IBinary DecryptString(string value);
}
