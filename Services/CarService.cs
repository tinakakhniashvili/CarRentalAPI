using CarRentalApp.Data;
using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Interfaces.Repositories;
using CarRentalApp.Models;

namespace CarRentalApp.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly ApplicationDbContext _context;

    public CarService(ICarRepository carRepository, ApplicationDbContext context)
    {
        _carRepository = carRepository;
        _context = context;
    }
    public IEnumerable<CarDTO> GetAllCars()
    {
        var cars = _carRepository.GetAllCars();
        return cars.Select(car => new CarDTO
        {
            Id = car.Id,
            Brand = car.Brand,
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
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            PricePerDay = car.PricePerDay
        };
    }

    public Car CreateCar(CreateCarDTO carDto)
    {
        var car = new Car
        {
            Brand = carDto.Brand,
            Model = carDto.Model,
            Year = carDto.Year,
            PricePerDay = carDto.PricePerDay,
            Description = carDto.Description,
            ImageUrl = carDto.ImageUrl,
            IsAvailable = carDto.IsAvailable,
        };

        _context.Cars.Add(car);
        _context.SaveChanges();

        return car;
    }

    public void DeleteCar(int carId)
    {
        var car = _carRepository.GetCarById(carId);

        if (car != null)
            _carRepository.DeleteCar(carId);
    }

    public IEnumerable<CarDTO> FilterCars(string? make, string? model, int? year, decimal? maxPrice, bool? isAvailable)
    {
        var cars = _carRepository.FilterCras(make, model, year, maxPrice, isAvailable);

        return cars.Select(car => new CarDTO
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            PricePerDay = car.PricePerDay,
            IsAvailable = car.IsAvailable ?? false,
            Description = car.Description,
            ImageUrl = car.ImageUrl
        }).ToList();
    }
}