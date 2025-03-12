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

    public List<Rental> GetRentalsByUserId(int userId)
    {
        return _context.Rentals.Include(r => r.Car).Where(r => r.UserId == userId).ToList();
    }

    public void AddRental(Rental rental)
    {
        _context.Rentals.Add(rental);
        _context.SaveChanges();
    }

    public void UpdateRental(Rental rental)
    {
        _context.Rentals.Update(rental);
        _context.SaveChanges();
    }

    public void DeleteRental(int rentalId)
    {
        var rental = _context.Rentals.Find(rentalId);
        if (rental != null)
        {
            _context.Rentals.Remove(rental);
            _context.SaveChanges();
        }
    }
}