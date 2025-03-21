using CarRentalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .Property(c => c.PricePerDay)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Rental>()
            .Property(r => r.TotalPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Car>().ToTable("Cars");
        modelBuilder.Entity<Rental>().ToTable("Rentals");
    }
}