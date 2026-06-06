using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class CreatePaymentRequest
{
    [Range(1, int.MaxValue)]
    public int RentalId { get; set; }

    [Range(0.01, 999999.99)]
    public decimal Amount { get; set; }

    public DateTime PaidAt { get; set; }
}