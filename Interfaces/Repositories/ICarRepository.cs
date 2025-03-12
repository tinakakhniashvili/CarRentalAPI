using CarRentalApp.Models;

namespace CarRentalApp.Interfaces.Repositories;

public interface ICarRepository
{
    List<Car> GetAllCars();
    Car GetCarById(int carId);
    void AddCar(Car car);
    void UpdateCar(Car car);
    void DeleteCar(int carId);
}