using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class SystemSetting
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string SettingKey { get; set; } = string.Empty;

    [Required]
    public string SettingValue { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsSensitive { get; set; } = false;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}