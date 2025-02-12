using Shared.Libraries;

namespace Shared.Security;

public class Pbkdf2Manager
{
    public byte[] Salt { get; private set; }
    public byte[] Value { get; private set; }

    public Pbkdf2Manager(
        byte[] salt, 
        byte[] value
    ) {
        this.Salt = salt;
        this.Value = value;
    }

    public static Binary Write(byte[] salt, byte[] value)
    {
        var bytes = new List<byte>();

        for (int x = 0; x < value.Length; x++)
        {
            if (x < salt.Length) bytes.Add(salt[x]);
            bytes.Add(value[x]);
        }

        return Binary.FromBytes(bytes.ToArray());
    }
    
    public static Pbkdf2Manager Read(
        byte[] value,
        int saltLength
    )
    {
        var salt = new List<byte>();
        var va = new List<byte>();

        for (int x = 0; x < value.Length; x++)
        {
            if (salt.Count < saltLength && (x == 0 || x % 2 == 0)) salt.Add(value[x]);
            else va.Add(value[x]);
        }
     
        return new Pbkdf2Manager(salt.ToArray(), va.ToArray());
    }
}