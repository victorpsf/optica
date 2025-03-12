using Shared.Interfaces;

namespace Shared.Libraries;

public class Binary: IBinary
{
    public byte[] Bytes { get; private set; }

    public Binary(byte[] bytes)
    {
        this.Bytes = bytes;
    }

    public IBinary Copy()
    {
        byte[] bytes = new byte[this.Bytes.Length];
        Array.Copy(this.Bytes, bytes, bytes.Length);
        return new Binary(bytes);
    }

    public string toHexString()
        => Convert.ToHexString(this.Bytes);
    
    public string toBase64String()
        => Convert.ToBase64String(this.Bytes);

    public string toBinaryString()
        => string.Join(
            "",
            this.Bytes.Select(a => Convert.ToChar(a))
                .Select(a => a.ToString())
                .ToArray()
        );

    public static Binary FromBase64(string base64)
        => new (Convert.FromBase64String(base64));

    public static Binary FromHex(string hex)
        => new (Convert.FromHexString(hex));

    public static byte[] ToByteArray(string text)
        => text.ToCharArray()
            .Select(c => Convert.ToByte(c))
            .ToArray();

    public static Binary FromString(string text)
        => new(
            text.ToCharArray()
                .Select(c => Convert.ToByte(c))
                .ToArray()
        );
    
    public static Binary FromBytes(byte[] bytes)
        => new(bytes);
}
