using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.DTOs;

public class RegisterDTO
{
    [Required]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }
}