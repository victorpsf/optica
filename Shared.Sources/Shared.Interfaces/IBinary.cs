
namespace Shared.Interfaces;

public interface IBinary
{
    byte[] Bytes { get; }

    IBinary Copy();

    string toHexString();

    string toBase64String();

    string toBinaryString();
}
