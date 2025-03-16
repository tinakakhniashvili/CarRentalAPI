using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public IActionResult GetAllcars()
    {
        var cars = _carService.GetAllCars();
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public IActionResult GetCarById(int id)
    {
        var car = _carService.GetCarById(id);
        if (car == null)
        {
            return NotFound(new { message = "Car not foound" });
        }

        return Ok(car);
    }

    [Authorize]
    [HttpPost]
    public IActionResult PostCar([FromBody] CreateCarDTO carDTO)
    {
        if (carDTO == null)
        {
            return BadRequest(new { message = "Invalid car data" });
        }

        var createdCar = _carService.CreateCar(carDTO);
        return CreatedAtAction(nameof(GetCarById), new { id = createdCar.Id }, createdCar);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteCar(int id)
    {
        var car = _carService.GetCarById(id);
        if (car == null)
        {
            return NotFound(new { message = "Car not found" });
        }
        
        _carService.DeleteCar(id);
        return NoContent();
    }

    [HttpGet("filter")]
    public IActionResult FilterCras([FromQuery] string? make, [FromQuery] string? model,
        [FromQuery] int? year, [FromQuery] decimal? maxPrice,
        [FromQuery] bool? isAvailable)
    {
        var filteredCars = _carService.FilterCars(make, model, year, maxPrice, isAvailable);
        return Ok(filteredCars);
    }
}