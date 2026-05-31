using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class DepositTransaction
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public Rental Rental { get; set; } = null!;

    [Required]
    [MaxLength(30)]
    public string TransactionType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public int? ChargeId { get; set; }

    public Charge? Charge { get; set; }

    public DateTime TransactionDate { get; set; }

    public bool Locked { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}