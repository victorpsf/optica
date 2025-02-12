using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Databases.Entities;

[Table(name: "users", Schema = "public")]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Active { get; set; }
    public bool ForcePasswordReset { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}