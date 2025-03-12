namespace Shared.Interfaces.Security;

public interface IJwt
{
    public IAuthToken Write(IClaimIdentifier claim);
    IClaimIdentifier Read(string token);
}
