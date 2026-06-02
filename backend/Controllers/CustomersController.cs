using backend.Data;
using backend.DTOs;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace backend.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _db;

    public CustomersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CustomerResponse>>> GetCustomers()
    {
        var customers = await _db.Customers
            .OrderBy(c => c.Id)
            .Select(c => new CustomerResponse
            {
                Id = c.Id,
                FullName = c.FullName,
                PhoneNumber = c.PhoneNumber,
                IdType = c.IdType,
                IdNumber = c.IdNumber,
                EmergencyContactName = c.EmergencyContactName,
                EmergencyContactPhone = c.EmergencyContactPhone,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .ToListAsync();

        return Ok(customers);
    }
    

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponse>> GetCustomerById(int id)
    {
        var customer = await _db.Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerResponse
            {
                Id = c.Id,
                FullName= c.FullName,
                PhoneNumber = c.PhoneNumber,
                IdType = c.IdType,
                IdNumber = c.IdNumber,
                EmergencyContactName = c.EmergencyContactName,
                EmergencyContactPhone = c.EmergencyContactPhone,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .FirstOrDefaultAsync();
        if (customer ==null)
        {
            return NotFound();
        }
        return Ok(customer);    
    }



    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerResponse>> CreateCustomer(CreateCustomerRequest request)
    {
        var customer = new Customer
        {
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            IdType = request.IdType,
            IdNumber = request.IdNumber,
            EmergencyContactName = request.EmergencyContactName,
            EmergencyContactPhone = request.EmergencyContactPhone,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        var response = new CustomerResponse
        {
            Id = customer.Id,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            IdType = customer.IdType,
            IdNumber = customer.IdNumber,
            EmergencyContactName = customer.EmergencyContactName,
            EmergencyContactPhone = customer.EmergencyContactPhone,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt
        };

        return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, response);
    }
}