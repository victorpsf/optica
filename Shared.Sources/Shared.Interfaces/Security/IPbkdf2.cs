
namespace Shared.Interfaces.Security;

public interface IPbkdf2
{
    byte[] RandomBytes(int size);

    byte[] Write(byte[] value);
    IBinary WriteString(string value);
    IBinary WriteBase64(string value);
    IBinary WriteHex(string value);

    bool Verify(byte[] derived, byte[] value);
}
