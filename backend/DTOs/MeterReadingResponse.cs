namespace backend.DTOs;

public class MeterReadingResponse
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public int BillingPeriodId { get; set; }

    public int StorageUnitId { get; set; }

    public decimal ReadingValue { get; set; }

    public string ReadingType { get; set; } = string.Empty;

    public bool Locked { get; set; }

    public DateTime ReadAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}