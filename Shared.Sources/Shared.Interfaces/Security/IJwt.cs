namespace Shared.Interfaces.Security;

public interface IJwt
{
    IAuthToken Write(IClaimIdentifier claim);
    IClaimIdentifier Read(string token);
}
