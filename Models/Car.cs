using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApp.Models;

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; } // Manufacturer of the car
    public string Model { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }

    [Column(TypeName = "decimal(18,2)")] 
    public decimal PricePerDay { get; set; }
    public bool? IsAvailable { get; set; }
    public string ImageUrl { get; set; }
    
    // Foreign key to the rental entity
    public ICollection<Rental> Rentals { get; set; }
}