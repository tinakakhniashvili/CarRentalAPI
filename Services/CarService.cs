using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Interfaces.Repositories;

namespace CarRentalApp.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    public IEnumerable<CarDTO> GetAllCars()
    {
        var cars = _carRepository.GetAllCars();
        return cars.Select(car => new CarDTO
        {
            Id = car.Id,
            Make = car.Make,
            Model = car.Model,
            Year = car.Year,
            PricePerDay = car.PricePerDay
        }).ToList();
    }

    public CarDTO GetCarById(int carId)
    {
        var car = _carRepository.GetCarById(carId);
        if (car == null) return null;

        return new CarDTO
        {
            Id = car.Id,
            Make = car.Make,
            Model = car.Model,
            Year = car.Year,
            PricePerDay = car.PricePerDay
        };
    }

    public void DeleteCar(int carId)
    {
        var car = _carRepository.GetCarById(carId);

        if (car != null)
            _carRepository.DeleteCar(carId);
    }
}