using System.Security.Cryptography;
using CarRentalApp.Data;
using CarRentalApp.DTOs;
using CarRentalApp.Helpers;
using CarRentalApp.Interfaces;
using CarRentalApp.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CarRentalApp.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly JwtHelper _jwtHelper;

    public AuthService(ApplicationDbContext context, JwtHelper jwtHelper)
    {
        _context = context;
        _jwtHelper = jwtHelper;
    }
    
    public bool RegisterUser(RegisterDTO registerDto)
    {
        if (_context.Users.Any(u => u.Email == registerDto.Email))
            return false;

        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: registerDto.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        var user = new User
        {
            Email = registerDto.Email,
            PasswordHash = hashedPassword,
            Role = "User"
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return true;
    }

    public AuthDTO LoginUser(LoginDTO loginDTO)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == loginDTO.Email);
        if (user == null)
            return null;

        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: loginDTO.Password,
            salt: new byte[128 / 8],
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        if (hashedPassword != user.PasswordHash)
            return null;

        var token = _jwtHelper.GenerateToken(user);

        return new AuthDTO
        {
            Token = token
            // Email = user.Email,
            // Role = user.Role
        };
    }
}