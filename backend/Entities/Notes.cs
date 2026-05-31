using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class Note
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string RecordType { get; set; } = string.Empty;

    public int RecordId { get; set; }

    [Required]
    public string NoteText { get; set; } = string.Empty;

    public bool IsHidden { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? HiddenAt { get; set; }
}