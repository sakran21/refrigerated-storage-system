namespace backend.DTOs;

public class RentalBalanceResponse
{
    public int RentalId { get; set; }

    public decimal TotalCharges { get; set; }

    public decimal TotalPayments { get; set; }

    public decimal AppliedDeposit { get; set; }

    public decimal OutstandingBalance { get; set; }
}