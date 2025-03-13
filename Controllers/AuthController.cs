using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDTO registerDto)
    {
        var success = _authService.RegisterUser(registerDto);

        if (!success)
            return BadRequest(new { message = "User already exists" });
    
        // return Ok(new { message = "Registration successful" });
        return CreatedAtAction(nameof(Register), new { message = "Registration successful" });
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        var result = _authService.LoginUser(loginDto);

        if (result == null)
            return Unauthorized(new {message = "Invalid credentials"});

        return Ok(result);
    }
}