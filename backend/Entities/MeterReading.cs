using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class MeterReading
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public Rental Rental { get; set; } = null!;

    public int BillingPeriodId { get; set; }

    public BillingPeriod BillingPeriod { get; set; } = null!;

    public int StorageUnitId { get; set; }

    public StorageUnit StorageUnit { get; set; } = null!;

    public decimal ReadingValue { get; set; }

    [Required]
    [MaxLength(30)]
    public string ReadingType { get; set; } = string.Empty;

    public bool Locked { get; set; } = true;

    public DateTime ReadAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}