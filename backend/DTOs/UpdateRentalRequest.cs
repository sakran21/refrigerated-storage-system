using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class UpdateRentalRequest
{
    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Range(0.01, 999999.99)]
    public decimal MonthlyRentAmount { get; set; }

    [Range(0, 999999.99)]
    public decimal DepositAmount { get; set; }

    public bool IsDelinquent { get; set; }
}