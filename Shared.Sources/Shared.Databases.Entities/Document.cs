using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Databases.Entities;

[Table(name: "documents", Schema = "public")]
public class Document
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("document")]
    public string Value { get; set; }

    [Column("document_type")]
    public DocumentType Type { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [JsonIgnore]
    public Person Person { get; set; }
}
