using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "permissions", Schema = "public")]
public class Permission
{
    [Key]
    [Column("permission_id")]
    public int PermissionId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [JsonIgnore]
    public List<RolePermission> RolePermissions { get; set; }
}