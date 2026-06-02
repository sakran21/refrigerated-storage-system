using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class UpdateStorageUnitRequest
{
    [Required]
    [MaxLength(50)]
    public string UnitCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "available";

    [Range(0.01, 999999.99)]
    public decimal MonthlyRentAmount { get; set; }

    public bool IsActive { get; set; } = true;
}