using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;

namespace CarRentalApp.Services;

public class CarService : ICarService
{
    public IEnumerable<CarDTO> GetAllCars()
    {
        throw new NotImplementedException();
    }

    public CarDTO GetCarById(int carId)
    {
        throw new NotImplementedException();
    }

    public void DeleteCar(int carId)
    {
        throw new NotImplementedException();
    }
}