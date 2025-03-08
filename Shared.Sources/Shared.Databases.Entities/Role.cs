using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "roles", Schema = "public")]
public class Role
{
    [Key]
    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    public List<RolePermission> RolePermissions { get; set; }

    [JsonIgnore]
    public List<UserRole> UserRoles { get; set; }
}