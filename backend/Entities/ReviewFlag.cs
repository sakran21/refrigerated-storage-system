using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class ReviewFlag
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string RecordType { get; set; } = string.Empty;

    public int? RecordId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ActionType { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string TriggerSource { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Status { get; set; } = "pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ReviewedAt { get; set; }

    public string? ReviewNote { get; set; }
}