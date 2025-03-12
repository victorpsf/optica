using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;


[Table(name: "contacts", Schema = "public")]
public class Contact
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("value")]
    public string Value { get; set; }

    [Column("contact_type")]
    public ContactType Type { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [JsonIgnore]
    public Person Person { get; set; }
}
