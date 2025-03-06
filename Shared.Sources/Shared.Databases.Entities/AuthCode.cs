
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "codes", Schema = "public")]
public class AuthCode
{
    public int? Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime ExpireIn { get; set; }
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public User User { get; set; }
}