using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "role_permission", Schema = "public")]
public class RolePermission
{
    [Key, Column("permission_id", Order = 0)]
    public int PermissionId { get; set; }

    [Key, Column("role_id", Order = 1)]
    public int RoleId { get; set; }

    public Permission Permission { get; set; }

    [JsonIgnore]
    public Role Role { get; set; }
}
