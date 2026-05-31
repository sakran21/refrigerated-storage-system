using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class AdminCorrection
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string RecordType { get; set; } = string.Empty;

    public int RecordId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CorrectionType { get; set; } = string.Empty;

    public string? OldValueJson { get; set; }

    public string? NewValueJson { get; set; }

    [Required]
    public string CorrectionReason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}