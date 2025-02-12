using System.Security.Claims;

namespace Shared.Models.Security;

public class ClaimIdentifier
{
    public int UserId { get; set; }
    
    public static ClaimIdentifier GetInstance(IEnumerable<Claim> claims)
    {
        var uI = claims.FirstOrDefault(a => a.Type == "uI");
        
        if (uI == null)
            throw new NullReferenceException();

        return new ClaimIdentifier()
        {
            UserId = Convert.ToInt32(uI.Value)
        };
    }
}