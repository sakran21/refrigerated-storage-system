using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class AuditLog
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string ActorRole { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ActionType { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string RecordType { get; set; } = string.Empty;

    public int? RecordId { get; set; }

    public string? OldValueJson { get; set; }

    public string? NewValueJson { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}