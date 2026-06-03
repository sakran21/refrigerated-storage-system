using backend.Data;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
}