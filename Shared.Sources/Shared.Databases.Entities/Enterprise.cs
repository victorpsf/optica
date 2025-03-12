using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table("enterprises", Schema = "public")]
public class Enterprise
{
    [Key]
    [Column("enterprise_id")]
    public int EnterpriseId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [JsonIgnore]
    public List<UserEnterprise>? UserEnterprises;
}
