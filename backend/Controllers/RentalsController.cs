using backend.Data;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Entities;
using System.Globalization;

namespace backend.Controllers;

[ApiController]
[Route("api/rentals")]
public class RentalsController : ControllerBase
{
    private readonly AppDbContext _db;

    public RentalsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<RentalResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RentalResponse>>> GetRentals()
    {
        var rentals = await _db.Rentals
            .OrderBy(r => r.Id)
            .Select(r => new RentalResponse
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                StorageUnitId = r.StorageUnitId,
                Status = r.Status,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                MonthlyRentAmount = r.MonthlyRentAmount,
                DepositAmount = r.DepositAmount,
                IsDelinquent = r.IsDelinquent,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            })
            .ToListAsync();

        return Ok(rentals);
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(List<RentalResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RentalResponse>>> GetActiveRentals()
    {
        var rentals = await _db.Rentals
            .Where(rental =>
                rental.Status == "active" &&
                rental.EndDate == null)
            .OrderBy(rental => rental.StartDate)
            .ThenBy(rental => rental.Id)
            .Select(rental => new RentalResponse
            {
                Id = rental.Id,
                CustomerId = rental.CustomerId,
                StorageUnitId = rental.StorageUnitId,
                Status = rental.Status,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                MonthlyRentAmount = rental.MonthlyRentAmount,
                DepositAmount = rental.DepositAmount,
                IsDelinquent = rental.IsDelinquent,
                CreatedAt = rental.CreatedAt,
                UpdatedAt = rental.UpdatedAt
            })
            .ToListAsync();

        return Ok(rentals);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RentalResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RentalResponse>> GetRentalById(int id)
    {
        var rental = await _db.Rentals
            .Where(r => r.Id == id)
            .Select(r => new RentalResponse
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                StorageUnitId = r.StorageUnitId,
                Status = r.Status,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                MonthlyRentAmount = r.MonthlyRentAmount,
                DepositAmount = r.DepositAmount,
                IsDelinquent = r.IsDelinquent,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (rental == null)
        {
            return NotFound();
        }

        return Ok(rental);
    }

    [HttpGet("{id}/balance")]
    [ProducesResponseType(typeof(RentalBalanceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RentalBalanceResponse>> GetRentalBalance(int id)
    {
        var rentalExists = await _db.Rentals.AnyAsync(r => r.Id ==id);

        if (!rentalExists)
        {
            return NotFound("Rental not found.");
        }

        var totalCharges = await _db.Charges
            .Where(c => c.RentalId == id)
            .SumAsync(c => (decimal?)c.Amount) ?? 0;

        var totalPayments = await _db.Payments
            .Where(p => p.RentalId == id)
            .SumAsync(p => (decimal?)p.Amount) ?? 0;

        var appliedDeposit = await _db.DepositTransactions
            .Where(d =>
                d.RentalId == id &&
                d.TransactionType == "applied")
            .SumAsync(d => (decimal?)d.Amount) ?? 0;

        var outstandingBalance = totalCharges - totalPayments - appliedDeposit;

        var response = new RentalBalanceResponse
        {
            RentalId = id,
            TotalCharges = totalCharges,
            TotalPayments = totalPayments,
            AppliedDeposit = appliedDeposit,
            OutstandingBalance = outstandingBalance
        };

        return Ok(response);
    }

    [HttpGet("{id}/payments")]
    [ProducesResponseType(typeof(List<PaymentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<PaymentResponse>>> GetRentalPayments(int id)
    {
        var rentalExists = await _db.Rentals.AnyAsync(rental => rental.Id == id);

        if (!rentalExists)
        {
            return NotFound("Rental not found.");
        }

        
        var payments = await _db.Payments
            .Where(payment => payment.RentalId == id)
            .OrderBy(payment => payment.PaidAt)
            .ThenBy(payment => payment.Id)
            .Select(payment => new PaymentResponse
            {
                Id = payment.Id,
                RentalId = payment.RentalId,
                Amount = payment.Amount,
                PaidAt = payment.PaidAt,
                Locked = payment.Locked,
                CreatedAt = payment.CreatedAt,
                UpdatedAt = payment.UpdatedAt 
                
            })
            .ToListAsync();

        return Ok(payments);
    }

    [HttpGet("{id}/charges")]
    [ProducesResponseType(typeof(List<ChargeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ChargeResponse>>> GetRentalCharges(int id)
    {
        var rentalExists = await _db.Rentals.AnyAsync(rental => rental.Id == id);

        if (!rentalExists)
        {
            return NotFound("Rental not found.");
        }

        var charges = await _db.Charges
            
            .Where(charge => charge.RentalId == id)
            .OrderBy(charge => charge.CreatedAt)
            .ThenBy(charge => charge.Id)
            .Select(charge => new ChargeResponse
            {
                Id = charge.Id,
                RentalId = charge.RentalId,
                BillingPeriodId = charge.BillingPeriodId,
                ChargeType = charge.ChargeType,
                Amount = charge.Amount,
                Status = charge.Status,
                IsOverridden = charge.IsOverridden,
                ElectricityRateSnapshot = charge.ElectricityRateSnapshot,
                Locked = charge.Locked,
                CreatedAt = charge.CreatedAt,
                UpdatedAt = charge.UpdatedAt
            })
            .ToListAsync();

        return Ok(charges);
    }

    [HttpGet("{id}/meter-readings")]
    [ProducesResponseType(typeof(List<MeterReadingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<MeterReadingResponse>>> GetRentalMeterReadings(int id)
    {
        var rentalExists = await _db.Rentals.AnyAsync(rental => rental.Id == id);

        if (!rentalExists)
        {
            return NotFound("Rental not found.");
        }

        var meterReadings = await _db.MeterReadings
            .Where(meterReading => meterReading.RentalId == id)
            .OrderBy(meterReading => meterReading.ReadAt)
            .ThenBy(meterReading => meterReading.Id)
            .Select(meterReading => new MeterReadingResponse
            {
                Id = meterReading.Id,
                RentalId = meterReading.RentalId,
                BillingPeriodId = meterReading.BillingPeriodId,
                StorageUnitId = meterReading.StorageUnitId,
                ReadingValue = meterReading.ReadingValue,
                ReadingType = meterReading.ReadingType,
                Locked = meterReading.Locked,
                ReadAt = meterReading.ReadAt,
                CreatedAt = meterReading.CreatedAt,
                UpdatedAt = meterReading.UpdatedAt
            })
            .ToListAsync();

        return Ok(meterReadings);
    }

    [HttpGet("{id}/billing-periods")]
    [ProducesResponseType(typeof(List<BillingPeriodResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BillingPeriodResponse>>> GetRentalBillingPeriods(int id)
    {
        var rentalExists = await _db.Rentals.AnyAsync(rental => rental.Id == id);

        if (!rentalExists)
        {
            return NotFound("Rental not found.");
        }

        var billingPeriods = await _db.BillingPeriods
            .Where(period => period.RentalId == id)
            .OrderBy(period => period.PeriodStartDate)
            .ThenBy(period => period.Id)
            .Select(period => new BillingPeriodResponse
            {
                Id = period.Id,
                RentalId = period.RentalId,
                PeriodStartDate = period.PeriodStartDate,
                PeriodEndDate = period.PeriodEndDate,
                DueDate = period.DueDate,
                Status = period.Status,
                CreatedAt = period.CreatedAt,
                UpdatedAt = period.UpdatedAt
            })
            .ToListAsync();

        return Ok(billingPeriods);
    }




    [HttpGet("{id}/deposits")]
    [ProducesResponseType(typeof(List<DepositTransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<DepositTransactionResponse>>> GetRentalDeposits(int id)
    {
        var rentalExists = await _db.Rentals.AnyAsync(rental => rental.Id == id);

        if (!rentalExists)
        {
            return NotFound("Rental not found.");
        }

        var deposits = await _db.DepositTransactions
            .Where(deposit => deposit.RentalId == id)
            .OrderBy(deposit => deposit.TransactionDate)
            .ThenBy(deposit => deposit.Id)
            .Select(deposit => new DepositTransactionResponse
            {
                Id = deposit.Id,
                RentalId = deposit.RentalId,
                TransactionType = deposit.TransactionType,
                Amount = deposit.Amount,
                ChargeId = deposit.ChargeId,
                TransactionDate = deposit.TransactionDate,
                Locked = deposit.Locked,
                CreatedAt = deposit.CreatedAt,
                UpdatedAt = deposit.UpdatedAt
            })
            .ToListAsync();

        return Ok(deposits);
    }



    [HttpPost]
    [ProducesResponseType(typeof(RentalResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<RentalResponse>> CreateRental(CreateRentalRequest request)
    {
        if (request.StartDate == default)
        {
            return BadRequest("Start date is required.");
        }

        var customerExists = await _db.Customers
            .AnyAsync(c => c.Id == request.CustomerId);

        if (!customerExists)
        {
            return NotFound("Customer not found.");
        }

        var storageUnit = await _db.StorageUnits
            .FirstOrDefaultAsync(s => s.Id == request.StorageUnitId);

        if (storageUnit == null)
        {
            return NotFound("Storage unit not found.");
        }

        var activeRentalExists = await _db.Rentals.AnyAsync(r =>
            r.StorageUnitId == request.StorageUnitId &&
            r.Status == "active" &&
            r.EndDate == null);

        if (activeRentalExists)
        {
            return Conflict("Storage unit already has an active rental.");
        }

        if (!storageUnit.IsActive)
        {
            return Conflict("Storage unit is inactive.");
        }

        if (!string.Equals(
            storageUnit.Status,
            "available",
            StringComparison.OrdinalIgnoreCase))
        {
            return Conflict("Storage unit is not available.");
        }

        var now = DateTime.UtcNow;
        var periodEndDate = request.StartDate.AddMonths(1);

        var rental = new Rental
        {
            CustomerId = request.CustomerId,
            StorageUnitId = request.StorageUnitId,
            Status = "active",
            StartDate = request.StartDate,
            EndDate = null,
            MonthlyRentAmount = request.MonthlyRentAmount,
            DepositAmount = request.DepositAmount,
            IsDelinquent = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        var billingPeriod = new BillingPeriod
        {
            Rental = rental,
            PeriodStartDate = request.StartDate,
            PeriodEndDate = periodEndDate,
            DueDate = periodEndDate,
            Status = "open",
            CreatedAt = now,
            UpdatedAt = now
        };

        var rentCharge = new Charge
        {
            Rental = rental,
            BillingPeriod = billingPeriod,
            ChargeType = "rent",
            Amount = request.MonthlyRentAmount,
            Status = "paid",
            IsOverridden = false,
            ElectricityRateSnapshot = null,
            Locked = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        var firstRentPayment = new Payment
        {
            Rental = rental,
            Amount = request.MonthlyRentAmount,
            PaidAt = now,
            Locked = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        DepositTransaction? depositTransaction = null;

        if (request.DepositAmount > 0)
        {
            depositTransaction = new DepositTransaction
            {
                Rental = rental,
                TransactionType = "credit",
                Amount = request.DepositAmount,
                ChargeId = null,
                TransactionDate = now,
                Locked = true,
                CreatedAt = now,
                UpdatedAt = now
            };
        }

        var startingMeterReading = new MeterReading
        {
            Rental = rental,
            BillingPeriod = billingPeriod,
            StorageUnit = storageUnit,
            ReadingValue = request.StartingMeterReadingValue,
            ReadingType = "start",
            Locked = true,
            ReadAt = request.StartDate,
            CreatedAt = now,
            UpdatedAt = now
        };

        storageUnit.Status = "rented";
        storageUnit.UpdatedAt = now;

        _db.Rentals.Add(rental);
        _db.BillingPeriods.Add(billingPeriod);
        _db.Charges.Add(rentCharge);
        _db.Payments.Add(firstRentPayment);
        _db.MeterReadings.Add(startingMeterReading);
        if (depositTransaction != null)
        {
            _db.DepositTransactions.Add(depositTransaction);
        }

        await _db.SaveChangesAsync();

        var response = new RentalResponse
        {
            Id = rental.Id,
            CustomerId = rental.CustomerId,
            StorageUnitId = rental.StorageUnitId,
            Status = rental.Status,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            MonthlyRentAmount = rental.MonthlyRentAmount,
            DepositAmount = rental.DepositAmount,
            IsDelinquent = rental.IsDelinquent,
            CreatedAt = rental.CreatedAt,
            UpdatedAt = rental.UpdatedAt
        };

        return CreatedAtAction(
            nameof(GetRentalById),
            new { id = rental.Id },
            response);
    }

    [HttpPost("{id}/close")]
    [ProducesResponseType(typeof(RentalResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<RentalResponse>> CloseRental(
        int id,
        CloseRentalRequest request)
    {
        var rental = await _db.Rentals
            .FirstOrDefaultAsync(r => r.Id == id);

        if (rental == null)
        {
            return NotFound("Rental not found.");
        }

        if (rental.Status == "closed")
        {
            return BadRequest("Rental is already closed.");
        }

        var now = DateTime.UtcNow;


        if (request.EndDate == default)
        {
            return BadRequest("End date is required.");
        }

        if (request.EndDate < rental.StartDate)
        {
            return BadRequest("End date cannot be before the rental start date.");
        }

        var storageUnit = await _db.StorageUnits
            .FirstOrDefaultAsync(s => s.Id == rental.StorageUnitId);

        if (storageUnit == null)
        {
            return Conflict("Rental storage unit could not be found.");
        }

        var billingPeriod = await _db.BillingPeriods
            .FirstOrDefaultAsync(b =>
                b.RentalId == rental.Id &&
                b.Status == "open");

        if (billingPeriod == null)
        {
            return Conflict("Rental does not have an open billing period.");
        }

        var latestMeterReading = await _db.MeterReadings
            .Where(m => m.RentalId == rental.Id)
            .OrderByDescending(m => m.ReadAt)
            .FirstOrDefaultAsync();

        if (latestMeterReading == null)
        {
            return Conflict("Rental does not have a previous meter reading.");
        }

        if (request.FinalMeterReadingValue < latestMeterReading.ReadingValue)
        {
            return BadRequest("Final meter reading cannot be lower than the previous reading.");
        }

        var electricityRateSetting = await _db.SystemSettings
            .FirstOrDefaultAsync(s =>
                s.SettingKey == "electricity_rate_per_unit");

        if (electricityRateSetting == null)
        {
            return Conflict("Electricity rate setting could not be found.");
        }

        if (!decimal.TryParse(
            electricityRateSetting.SettingValue,
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out var electricityRate))
        {
            return Conflict("Electricity rate setting is invalid.");
        }

        if (electricityRate < 0)
        {
            return Conflict("Electricity rate cannot be negative.");
        }
        
        var electricityUsage = request.FinalMeterReadingValue - latestMeterReading.ReadingValue;
        var electricityChargeAmount = electricityUsage * electricityRate;

        if (request.EndDate > now)
        {
            return BadRequest("End date cannot be in the future.");
        }

        var finalMeterReading = new MeterReading
        {
            Rental = rental,
            BillingPeriod = billingPeriod,
            StorageUnit = storageUnit,
            ReadingValue = request.FinalMeterReadingValue,
            ReadingType = "closure",
            Locked = true,
            ReadAt = request.EndDate,
            CreatedAt = now,
            UpdatedAt = now
        };

        var electricityCharge = new Charge
        {
            Rental = rental,
            BillingPeriod = billingPeriod,
            ChargeType = "electricity",
            Amount = electricityChargeAmount,
            Status = electricityChargeAmount == 0 ? "paid" : "open",
            IsOverridden = false,
            ElectricityRateSnapshot = electricityRate,
            Locked = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        rental.Status = "closed";
        rental.EndDate = request.EndDate;
        rental.UpdatedAt = now;

        billingPeriod.Status = "closed";
        billingPeriod.PeriodEndDate = request.EndDate;
        billingPeriod.UpdatedAt = now;

        storageUnit.Status = "available";
        storageUnit.UpdatedAt = now;

        _db.MeterReadings.Add(finalMeterReading);
        _db.Charges.Add(electricityCharge);

        await _db.SaveChangesAsync();



        var response = new RentalResponse
        {
            Id = rental.Id,
            CustomerId = rental.CustomerId,
            StorageUnitId = rental.StorageUnitId,
            Status = rental.Status,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            MonthlyRentAmount = rental.MonthlyRentAmount,
            DepositAmount = rental.DepositAmount,
            IsDelinquent = rental.IsDelinquent,
            CreatedAt = rental.CreatedAt,
            UpdatedAt = rental.UpdatedAt
        };

        return Ok(response);
    }
}