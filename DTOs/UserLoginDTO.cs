namespace CarRentalApp.DTOs;

public class UserLoginDTO
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public bool StaySignedIn { get; set; }
}