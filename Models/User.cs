using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    [Required]
    public string Role { get; set; }
    public DateTime DateJoined { get; set; }
    
    // Foreign key to Rentals
    public ICollection<Rental> Rentals { get; set; }
}