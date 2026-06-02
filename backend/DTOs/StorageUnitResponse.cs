namespace backend.DTOs;

public class StorageUnitResponse
{
    public int Id { get; set; }

    public string UnitCode { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public decimal MonthlyRentAmount { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}