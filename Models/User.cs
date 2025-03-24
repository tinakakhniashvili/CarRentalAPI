using System.ComponentModel.DataAnnotations;
using CarRentalApp.Entities;

namespace CarRentalApp.Models;

public class User : BaseClass
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
    public List<Role> Roles { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshExpirationDate { get; set; }
    public DateTime DateJoined { get; set; }
    
    // Foreign key to Rentals
    public ICollection<Rental> Rentals { get; set; }
}