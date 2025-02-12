using Shared.Libraries;

namespace Shared.Security.Managers;

public class SymmetricManager
{
    public byte[] iv { get; private set; }
    public byte[] value { get; private set; }

    public SymmetricManager(byte[] iv, byte[] value)
    {
        this.iv = iv;
        this.value = value;
    }

    public static Binary Write(byte[] iv, byte[] value)
    {
        var bytes = new List<byte>();

        for (int x = 0; x < value.Length; x++)
        {
            if (x < iv.Length) bytes.Add(iv[x]);
            bytes.Add(value[x]);
        }

        return Binary.FromBytes(bytes.ToArray());
    }
    
    public static SymmetricManager Read(
        byte[] value,
        int ivLength
    )
    {
        var iv = new List<byte>();
        var va = new List<byte>();

        for (int x = 0; x < value.Length; x++)
        {
            if (iv.Count < ivLength && (x == 0 || x % 2 == 0)) iv.Add(value[x]);
            else va.Add(value[x]);
        }
     
        return new SymmetricManager(iv.ToArray(), va.ToArray());
    }
}