namespace backend.DTOs;

public class RentalResponse
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int StorageUnitId { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal MonthlyRentAmount { get; set; }

    public decimal DepositAmount { get; set; }

    public bool IsDelinquent { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}