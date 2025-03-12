
namespace Shared.Interfaces.Security;

public interface IClaimIdentifier
{
    int UserId { get; }
    int EnterpriseId { get; }
    string[] Permissions { get; }
    string[] Roles { get; }
}
