using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class CloseRentalRequest
{
    public DateTime EndDate { get; set; }

    [Range(0, 999999999.99)]
    public decimal FinalMeterReadingValue { get; set; }
}