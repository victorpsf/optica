using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Databases.Entities;

[Table(name: "persons", Schema = "public")]
public class Person
{
    [Key]
    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("birth_date")]
    public DateTime BirthDate { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public List<Address> Addresses { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Document> Documents { get; set; }
}
