using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "user_role", Schema = "public")]
public class UserRole
{
    [Key, Column("user_id", Order = 0)]
    public int UserId { get; set; }

    [Key, Column("role_id", Order = 1)]
    public int RoleId { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    public Role Role { get; set; }
}
