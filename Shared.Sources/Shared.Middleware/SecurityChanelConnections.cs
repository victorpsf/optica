using System.Security.Cryptography;
using Shared.Libraries;
using Shared.Models.Security;
using Shared.Security;

namespace Shared.Middleware;

public class SecurityChanelConnections
{
    private List<SecurityChanel> Chanels = new();
    private RsaCryptographySize Size;
    
    public SecurityChanelConnections(RsaCryptographySize size)
    { this.Size = size; }

    public SecurityChanel? GetChanel(string id)
        => this.Chanels.Where(a => a.Identification.Equals(id)).FirstOrDefault();

    public SecurityChanel SetChanel(string id, byte[] clientPublicKey)
    {
        var chanel = this.GetChanel(id);

        if (chanel is null)
        {
            var rsa = RsaCryptography.Create(this.Size);
            chanel = new()
            {
                Identification = id,
                ClientPublicKey = clientPublicKey,
                ServerPublicKey = rsa.Keys.PublicKey,
                ServerPrivateKey = rsa.Keys.PrivateKey,
                LastUsaged = DateTime.Now
            };

            this.Chanels.Add(chanel);
        }
        
        else if (Binary.FromBytes(chanel.ClientPublicKey).toBase64String() != Binary.FromBytes(clientPublicKey).toBase64String())
            chanel.ClientPublicKey = clientPublicKey;

        return chanel;
    }

    public void RemoveChanels(DateTime limit)
    {
        var persist = this.Chanels.Where(a => a.LastUsaged >= limit);
        this.Chanels.Clear();
        this.Chanels.AddRange(persist);
    }

    public void RemoveChanel(string id)
    {
        var chanel = this.GetChanel(id);
        this.Chanels.Remove(chanel);
    }
}