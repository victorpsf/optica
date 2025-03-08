
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "codes", Schema = "public")]
public class AuthCode
{
    [Key]
    [Column("code_id")]
    public int CodeId { get; set; }

    [Column("code")]
    public string Code { get; set; }

    [Column("expire_in")]
    public DateTime ExpireIn { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; }
}