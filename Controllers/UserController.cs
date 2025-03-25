using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        if (users == null)
            return NotFound("No users found");

        return Ok(users);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        var updatedUser = _userService.UpdateUser(id, userDTO);
        return Ok(updatedUser);
    }
}