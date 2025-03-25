using CarRentalApp.DTOs;
using CarRentalApp.Models;

namespace CarRentalApp.Interfaces;

public interface ICarService
{
    IEnumerable<CarDTO> GetAllCars();
    CarDTO GetCarById(int carId);
    Car CreateCar(CreateCarDTO carDto);
    void DeleteCar(int carId);
    IEnumerable<CarDTO> FilterCars(string? make, string? model, int? year, decimal? maxPrice, bool? isAvailable);
}
