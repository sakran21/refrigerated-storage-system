namespace backend.DTOs;

public class ChargeResponse
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public int BillingPeriodId { get; set; }

    public string ChargeType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public bool IsOverridden { get; set; }

    public decimal? ElectricityRateSnapshot { get; set; }

    public bool Locked { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set;}
}