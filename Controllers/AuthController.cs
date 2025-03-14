using CarRentalApp.Data;
using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ApplicationDbContext _context;

    public AuthController(IAuthService authService, ApplicationDbContext context)
    {
        _authService = authService;
        _context = context;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDTO request)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }

        var user = new User
        {
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = "User"
        };
        _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDTO request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
        if (user == null)
        {
            return BadRequest("User Not Found!");
        }

        if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong Password");
        }

        var token = _authService.CreateToken(user);
        return Ok(new 
        { 
            token,
            user.FirstName,
            user.LastName,
            user.Role,
            user.PhoneNumber,
            user.Email });
    }
}