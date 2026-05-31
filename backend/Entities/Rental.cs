using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class Rental
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public int StorageUnitId { get; set; }

    public StorageUnit StorageUnit { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "active";

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal MonthlyRentAmount { get; set; }

    public decimal DepositAmount { get; set; }

    public bool IsDelinquent { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}