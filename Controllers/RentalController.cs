using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpGet("{id}")]
    public IActionResult GetRentalById(int id)
    {
        var rental = _rentalService.GetRentalById(id);
        if (rental == null)
        {
            return NotFound(new { message = "Rental not found" });
        }

        return Ok(rental);
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public IActionResult GetRentalByUserId(int userId)
    {
        var rentals = _rentalService.GetRentalByUserId(userId);
        return Ok(rentals);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateRental([FromBody] CreateRentalDTO rentalDto)
    {
        if (rentalDto == null)
            return BadRequest(new { message = "Invalid rental data" });

        var rental = _rentalService.CreateRental(rentalDto);
        return CreatedAtAction(nameof(GetRentalById), new { id = rental.Id }, rental);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteRental(int id)
    {
        var rental = _rentalService.GetRentalById(id);
        
        if (rental == null)
            return NotFound(new { message = "Rental not found" });
        
        _rentalService.DeleteRental(id);
        return NoContent();
    }
}