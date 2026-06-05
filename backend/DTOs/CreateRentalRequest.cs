using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class CreateRentalRequest
{
    [Range(1, int.MaxValue)]
    public int CustomerId {get; set;}

    [Range(1, int.MaxValue)]
    public int StorageUnitId{get; set;}

    public DateTime StartDate { get; set;}

    [Range(0.01,999999.99)]
    public decimal MonthlyRentAmount{ get; set;}

    [Range(0, 999999.99)]
    public decimal DepositAmount{ get; set;}

    [Range(0, 999999999.99)]
    public decimal StartingMeterReadingValue { get; set; }
}