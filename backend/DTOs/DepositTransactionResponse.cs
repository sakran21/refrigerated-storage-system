namespace backend.DTOs;

public class DepositTransactionResponse
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public int? ChargeId { get; set; }

    public DateTime TransactionDate { get; set; }

    public bool Locked { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt {get; set;}
}