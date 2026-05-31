using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class Charge
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public Rental Rental { get; set; } = null!;

    public int BillingPeriodId { get; set; }

    public BillingPeriod BillingPeriod { get; set; } = null!;

    [Required]
    [MaxLength(30)]
    public string ChargeType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    [Required]
    [MaxLength(30)]
    public string Status { get; set; } = "open";

    public bool IsOverridden { get; set; } = false;

    public decimal? ElectricityRateSnapshot { get; set; }

    public bool Locked { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}