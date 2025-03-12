namespace Shared.Models.Security;

public class SecurityChanel
{
    public string Identification { get; set; } = string.Empty;
    public byte[] ClientPublicKey { get; set; }
    public byte[] ServerPublicKey { get; set; }
    public byte[] ServerPrivateKey { get; set; }
    public DateTime LastUsaged { get; set; } = DateTime.Now;
}