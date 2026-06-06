using backend.Data;
using backend.DTOs;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public PaymentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<PaymentResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PaymentResponse>>> GetPayments()
    {
        var payments = await _db.Payments
            .OrderBy(p => p.Id)
            .Select(p => new PaymentResponse
            {
                Id = p.Id,
                RentalId = p.RentalId,
                Amount = p.Amount,
                PaidAt = p.PaidAt,
                Locked = p.Locked,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync();

        return Ok(payments);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentResponse>> GetPaymentById(int id)
    {
        var payment = await _db.Payments
            .Where(p => p.Id == id)
            .Select(p => new PaymentResponse
            {
                Id = p.Id,
                RentalId = p.RentalId,
                Amount = p.Amount,
                PaidAt = p.PaidAt,
                Locked = p.Locked,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (payment == null)
        {
            return NotFound();
        }

        return Ok(payment);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentResponse>> CreatePayment(CreatePaymentRequest request)
    {
        if (request.PaidAt == default)
        {
            return BadRequest("Payment date is required.");
        }

        var rentalExists = await _db.Rentals
            .AnyAsync(r => r.Id == request.RentalId);

        if (!rentalExists)
        {
            return NotFound("Rental not found.");
        }

        var now = DateTime.UtcNow;

        var payment = new Payment
        {
            RentalId = request.RentalId,
            Amount = request.Amount,
            PaidAt = request.PaidAt,
            Locked = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();

        var response = new PaymentResponse
        {
            Id = payment.Id,
            RentalId = payment.RentalId,
            Amount = payment.Amount,
            PaidAt = payment.PaidAt,
            Locked = payment.Locked,
            CreatedAt = payment.CreatedAt,
            UpdatedAt = payment.UpdatedAt
        };

        return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, response);
    }
}