using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<StorageUnit> StorageUnits => Set<StorageUnit>();

    public DbSet<Rental> Rentals => Set<Rental>();

    public DbSet<BillingPeriod> BillingPeriods => Set<BillingPeriod>();

    public DbSet<Charge> Charges => Set<Charge>();

    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<MeterReading> MeterReadings => Set<MeterReading>();
}