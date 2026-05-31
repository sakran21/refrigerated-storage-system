using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class StorageUnit
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string UnitCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "available";

    public decimal MonthlyRentAmount { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}