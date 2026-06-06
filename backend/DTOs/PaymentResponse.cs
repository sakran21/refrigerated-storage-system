namespace backend.DTOs;

public class PaymentResponse
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaidAt { get; set; }

    public bool Locked { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}