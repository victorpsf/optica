using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Shared.Interfaces;
using Shared.Interfaces.Security;
using Shared.Libraries;
using Shared.Models.Security;

namespace Shared.Security;

public class Pbkdf2: IPbkdf2
{
    private Pbkdf2Size Size;
    private Pbkdf2HashDerivation HashDerivation;

    private Pbkdf2(
        Pbkdf2Size size,
        Pbkdf2HashDerivation HashDerivation
    )
    {
        this.Size = size;
        this.HashDerivation = HashDerivation;
    }

    public static Pbkdf2 Create(
        Pbkdf2Size size,
        Pbkdf2HashDerivation HashDerivation
    ) => new(size, HashDerivation);
    
    public static Pbkdf2 Create() 
        => new(Pbkdf2Size._8192, Pbkdf2HashDerivation.HMACSHA512);

    private int InteractionCount
    {
        get => this.HashDerivation switch
        {
            Pbkdf2HashDerivation.HMACSHA512 => 210000, // ENTRE 100000 A 1000000
            Pbkdf2HashDerivation.HMACSHA256 => 600000,
            Pbkdf2HashDerivation.HMACSHA1 => 1300000,
            _ => 210000,
        };
    }

    private KeyDerivationPrf KeyDerivationPrf
    {
        get => this.HashDerivation switch
        {
            Pbkdf2HashDerivation.HMACSHA512 => KeyDerivationPrf.HMACSHA512,
            Pbkdf2HashDerivation.HMACSHA256 => KeyDerivationPrf.HMACSHA256,
            Pbkdf2HashDerivation.HMACSHA1 => KeyDerivationPrf.HMACSHA1,
            _ => throw new ArgumentException(
                $"[ERROR HashDerivation] Pbkdf2Security: {HashDerivation.ToString()} is not defined")
        };
    }

    private int SizeLen
    { get => Convert.ToInt32(this.Size); }
        
    private byte[] DeriveValue(string value, byte[] salt, Pbkdf2HashDerivation hashDerivation)
        => KeyDerivation.Pbkdf2(
            password: value, 
            salt: salt, 
            prf: this.KeyDerivationPrf, 
            iterationCount: this.InteractionCount, 
            numBytesRequested: this.SizeLen
        );
    
    public byte[] RandomBytes(int size)
    {
        byte[] value = new byte[size];
        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(value);
        return value;
    }

    public byte[] Write(byte[] value)
    {
        var salt = this.RandomBytes(this.SizeLen / 2);
        var result = this.DeriveValue(
            Binary.FromBytes(Hash.Create(HashCipherMode.SHA512).Update(value)).toBase64String(), 
            salt, 
            this.HashDerivation
        );

        return Pbkdf2Manager.Write(salt, result).Bytes;
    }
    
    public IBinary WriteString(string value)
        => Binary.FromBytes(this.Write(Binary.FromString(value).Bytes));
    
    public IBinary WriteBase64(string value)
        => Binary.FromBytes(this.Write(Binary.FromBase64(value).Bytes));
    
    public IBinary WriteHex(string value)
        => Binary.FromBytes(this.Write(Binary.FromHex(value).Bytes));

    public bool Verify(byte[] derived, byte[] value)
    {
        var info = Pbkdf2Manager.Read(derived, this.SizeLen / 2);

        var result = this.DeriveValue(
            Binary.FromBytes(Hash.Create(HashCipherMode.SHA512).Update(value)).toBase64String(), 
            info.Salt, 
            this.HashDerivation
        );
        
        return Binary.FromBytes(info.Value).toBase64String() == Binary.FromBytes(result).toBase64String();
    }
}