using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class Payment
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public Rental Rental { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime PaidAt { get; set; }

    public bool Locked { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}