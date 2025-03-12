using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table("user_enterprise", Schema = "public")]
public class UserEnterprise
{
    [Key, Column("user_id", Order = 0)]
    public int UserId { get; set; }

    [Key, Column("enterprise_id", Order = 1)]
    public int EntepriseId { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    public Enterprise Enterprise { get; set; }
}
