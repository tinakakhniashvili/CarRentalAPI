namespace CarRentalApp.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string Role { get; set; }
    public DateTime DateJoined { get; set; }
    
    // Foreign key to Rentals
    public ICollection<Rental> Rentals { get; set; }
}