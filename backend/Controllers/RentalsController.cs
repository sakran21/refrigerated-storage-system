using backend.Data;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Entities;

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

    [HttpPost]
    [ProducesResponseType(typeof(RentalResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RentalResponse>> CreateRental(CreateRentalRequest request)
    {
        var customerExists = await _db.Customers.AnyAsync(c => c.Id == request.CustomerId);

        if (!customerExists)
        {
            return NotFound("Customer not found.");
        }

        var storageUnit = await _db.StorageUnits.FirstOrDefaultAsync(s => s.Id == request.StorageUnitId);

        if (storageUnit == null)
        {
            return NotFound("Storage unit not found.");
        }

        if (!storageUnit.IsActive)
        {
            return BadRequest("Storage unit is inactive.");
        }

        if (storageUnit.Status != "available")
        {
            return BadRequest("Storage unit is not available.");
        }

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
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

        storageUnit.Status = "rented";
        storageUnit.UpdatedAt = DateTime.UtcNow;

        _db.Rentals.Add(rental);
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

        return CreatedAtAction(nameof(GetRentalById), new { id = rental.Id }, response);
    }
}