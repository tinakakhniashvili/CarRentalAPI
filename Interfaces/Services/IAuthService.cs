using CarRentalApp.DTOs;

namespace CarRentalApp.Interfaces;

public interface IAuthService
{
    bool RegisterUser(RegisterDTO registerDto);
    AuthDTO LoginUser(LoginDTO loginDTO);
}