using CarRentalApp.Interfaces.Repositories;
using CarRentalApp.Models;

namespace CarRentalApp.Repositories;

public class CarRepository : ICarRepository
{
    public List<Car> GetAllCars()
    {
        throw new NotImplementedException();
    }

    public Car GetCarById(int carId)
    {
        throw new NotImplementedException();
    }

    public void AddCarById(Car car)
    {
        throw new NotImplementedException();
    }

    public void UpdateCar(Car car)
    {
        throw new NotImplementedException();
    }

    public void DeleteCar(int carId)
    {
        throw new NotImplementedException();
    }
}