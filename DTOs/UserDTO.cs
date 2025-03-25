using CarRentalApp.Models;

namespace CarRentalApp.DTOs;

public class UserDTO
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<RentalDTO> Rentals { get; set; } = new List<RentalDTO>();
}