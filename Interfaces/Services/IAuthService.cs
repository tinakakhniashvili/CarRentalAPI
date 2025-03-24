using CarRentalApp.DTOs;
using CarRentalApp.Models;

namespace CarRentalApp.Interfaces;

public interface IAuthService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    
    Task<ServiceResponse<int>> Register(UserRegisterDTO registerDTO);
    Task<ServiceResponse<string>> Login(UserLoginDTO loginDTO);
}