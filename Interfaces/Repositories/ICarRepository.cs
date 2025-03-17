using CarRentalApp.Models;

namespace CarRentalApp.Interfaces.Repositories;

public interface ICarRepository
{
    List<Car> GetAllCars();
    Car GetCarById(int carId);
    void DeleteCar(int carId);
    public IEnumerable<Car> FilterCras(string? make, string? model, int? year, decimal? maxPrice, bool? isAvailable);
}