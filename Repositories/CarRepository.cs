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

    public void DeleteCar(int carId)
    {
        var car = _context.Cars.Find(carId);
        if (car != null)
        {
            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Car> FilterCras(string? make, string? model, int? year, decimal? maxPrice, bool? isAvailable)
    {
        var query = _context.Cars.AsQueryable();

        if (!string.IsNullOrEmpty(make))
            query = query.Where(c => c.Brand.ToLower() == make.ToLower());

        if (!string.IsNullOrEmpty(model))
            query = query.Where(c => c.Model.ToLower() == model.ToLower());

        if (year.HasValue)
            query = query.Where(c => c.Year == year.Value);

        if (maxPrice.HasValue)
            query = query.Where(c => c.PricePerDay <= maxPrice.Value);

        if (isAvailable.HasValue)
            query = query.Where(c => c.IsAvailable == isAvailable.Value);

        return query.ToList();
    }
}