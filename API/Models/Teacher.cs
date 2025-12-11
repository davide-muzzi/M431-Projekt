using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace API.Models;

[Table("lehrperson")]
public class Teacher : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("vorname")]
    public string Vorname { get; set; } = string.Empty;

    [Column("nachname")]
    public string Nachname { get; set; } = string.Empty;

    [Column("e_mail")]
    public string Email { get; set; } = string.Empty;
}
