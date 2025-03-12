using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.DTOs;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }
}