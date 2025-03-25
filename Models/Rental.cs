namespace CarRentalApp.Models;

public class Rental
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string RentalStatus { get; set; } 

    // Foreign key to the User entity
    public int UserId { get; set; }
    public User User { get; set; }
    
    // Foreign ke to the Car entitiy
    public int CarId { get; set; }
    public Car Car { get; set; }
}