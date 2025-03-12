using CarRentalApp.Data;
using CarRentalApp.Interfaces.Repositories;
using CarRentalApp.Models;

namespace CarRentalApp.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _context;

    public CarRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Car> GetAllCars()
    {
        return _context.Cars.ToList();
    }

    public Car GetCarById(int carId)
    {
        return _context.Cars.FirstOrDefault(c => c.Id == carId);
    }

    public void AddCar(Car car)
    {
        _context.Cars.Add(car);
        _context.SaveChanges();
    }

    public void UpdateCar(Car car)
    {
        _context.Cars.Update(car);
        _context.SaveChanges();
    }

    public void DeleteCar(int carId)
    {
        var car = _context.Cars.Find(carId);
        if (car != null)
        {
            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
    }
}