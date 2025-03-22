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
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.User)
            .WithMany(u => u.Rentals)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Car)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}