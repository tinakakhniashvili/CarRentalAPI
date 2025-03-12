using CarRentalApp.Data;
using CarRentalApp.Interfaces.Repositories;
using CarRentalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly ApplicationDbContext _context;

    public RentalRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public Rental GetRentalById(int rentalId)
    {
        return _context.Rentals.Include(r => r.Car).Include(r => r.User)
            .FirstOrDefault(r => r.Id == rentalId);
    }

    public List<Rental> GetRentalByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public void AddRental(Rental rental)
    {
        throw new NotImplementedException();
    }

    public void UpdateRental(Rental rental)
    {
        throw new NotImplementedException();
    }

    public void DeleteRental(int rentalId)
    {
        throw new NotImplementedException();
    }
}