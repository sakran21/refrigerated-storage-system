using backend.Data;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Entities;

namespace backend.Controllers;

[ApiController]
[Route("api/storageunits")]
public class StorageUnitsController : ControllerBase
{
    private readonly AppDbContext _db;

    public StorageUnitsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<StorageUnitResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StorageUnitResponse>>> GetStorageUnits()
    {
        var storageUnits = await _db.StorageUnits
            .OrderBy(s => s.Id)
            .Select(s => new StorageUnitResponse
            {
                Id = s.Id,
                UnitCode = s.UnitCode,
                Category = s.Category,
                Status = s.Status,
                MonthlyRentAmount = s.MonthlyRentAmount,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .ToListAsync();

        return Ok(storageUnits);
    }
    

    [HttpGet("available")]
    [ProducesResponseType(typeof(List<StorageUnitResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StorageUnitResponse>>> GetAvailableStorageUnits()
    {
        var storageUnits = await _db.StorageUnits
            .Where(storageUnit =>
                storageUnit.IsActive &&
                storageUnit.Status == "available")
            .OrderBy(storageUnit => storageUnit.UnitCode)
            .Select(storageUnit => new StorageUnitResponse
            {
                Id = storageUnit.Id,
                UnitCode = storageUnit.UnitCode,
                Category = storageUnit.Category,
                Status = storageUnit.Status,
                MonthlyRentAmount = storageUnit.MonthlyRentAmount,
                IsActive = storageUnit.IsActive,
                CreatedAt = storageUnit.CreatedAt,
                UpdatedAt = storageUnit.UpdatedAt
            })
            .ToListAsync();

        return Ok(storageUnits);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StorageUnitResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StorageUnitResponse>> GetStorageUnitById(int id)
    {
        var storageUnit = await _db.StorageUnits
            .Where(s => s.Id == id)
            .Select(s => new StorageUnitResponse
            {
                Id = s.Id,
                UnitCode = s.UnitCode,
                Category = s.Category,
                Status = s.Status,
                MonthlyRentAmount = s.MonthlyRentAmount,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (storageUnit == null)
        {
            return NotFound();
        }

        return Ok(storageUnit);
    }

    [HttpPost]
    [ProducesResponseType(typeof(StorageUnitResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StorageUnitResponse>> CreateStorageUnit(CreateStorageUnitRequest request)
    {
        var storageUnit = new StorageUnit
        {
            UnitCode = request.UnitCode,
            Category = request.Category,
            Status = request.Status,
            MonthlyRentAmount = request.MonthlyRentAmount,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.StorageUnits.Add(storageUnit);
        await _db.SaveChangesAsync();

        var response = new StorageUnitResponse
        {
            Id = storageUnit.Id,
            UnitCode = storageUnit.UnitCode,
            Category = storageUnit.Category,
            Status = storageUnit.Status,
            MonthlyRentAmount = storageUnit.MonthlyRentAmount,
            IsActive = storageUnit.IsActive,
            CreatedAt = storageUnit.CreatedAt,
            UpdatedAt = storageUnit.UpdatedAt
        };

        return CreatedAtAction(nameof(GetStorageUnitById), new { id = storageUnit.Id }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StorageUnitResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StorageUnitResponse>> UpdateStorageUnit(int id, UpdateStorageUnitRequest request)
    {
        var storageUnit = await _db.StorageUnits.FirstOrDefaultAsync(s => s.Id == id);

        if (storageUnit == null)
        {
            return NotFound();
        }

        storageUnit.UnitCode = request.UnitCode;
        storageUnit.Category = request.Category;
        storageUnit.Status = request.Status;
        storageUnit.MonthlyRentAmount = request.MonthlyRentAmount;
        storageUnit.IsActive = request.IsActive;
        storageUnit.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        var response = new StorageUnitResponse
        {
            Id = storageUnit.Id,
            UnitCode = storageUnit.UnitCode,
            Category = storageUnit.Category,
            Status = storageUnit.Status,
            MonthlyRentAmount = storageUnit.MonthlyRentAmount,
            IsActive = storageUnit.IsActive,
            CreatedAt = storageUnit.CreatedAt,
            UpdatedAt = storageUnit.UpdatedAt
        };

        return Ok(response);
    }
    
    
}