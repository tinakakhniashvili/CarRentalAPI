namespace CarRentalApp.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public DateTime DateJoined { get; set; }
    
    // Foreign key to Rentals
    public ICollection<Rental> Rentals { get; set; }
}