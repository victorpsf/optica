using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "users", Schema = "public")]
public class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("passphrase")]
    public string Password { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("force_password_reset")]
    public bool ForcePasswordReset { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [JsonIgnore]
    public List<AuthCode> AuthCodes { get; set; }

    public List<UserRole> Roles { get; set; }

    public string[] PermissionNames
    {
        get
        {
            if (this.Roles is null)
                return new string[0];

            var permissionNames = new List<string>();

            foreach (var userRole in this.Roles)
                if (userRole?.Role?.RolePermissions is not null)
                    foreach (var rolePermission in userRole.Role.RolePermissions)
                        if (rolePermission?.Permission?.Name is not null)
                            permissionNames.Add(rolePermission.Permission.Name);

            return permissionNames.ToArray();
        }
    }

    public string[] RoleNames
    {
        get
        {
            if (this.Roles is null)
                return new string[0];

            var permissionNames = new List<string>();

            foreach (var userRole in this.Roles)
                if (userRole?.Role is not null && userRole.Role.Name is not null)
                    permissionNames.Add(userRole.Role.Name);

            return permissionNames.ToArray();
        }
    }
}