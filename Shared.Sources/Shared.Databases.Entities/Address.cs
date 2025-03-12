using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "addresses", Schema = "public")]
public class Address
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("value")]
    public string Value { get; set; }

    [Column("address_type")]
    public AddressType Type { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [JsonIgnore]
    public Person Person { get; set; }
}
