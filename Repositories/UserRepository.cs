using CarRentalApp.Data;
using CarRentalApp.Interfaces.Repositories;
using CarRentalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public User GetUserByEmail(string email)
    {
       return _context.Users.FirstOrDefault(u => u.Email == email);
    }
    
    public User? GetUserById(int userId)
    {
        return _context.Users
            .Include(u => u.Rentals)
            .ThenInclude(r => r.Car)
            .FirstOrDefault(u => u.Id == userId);
    }
    public List<User> GetAllUsers()
    {
        return _context.Users
            .Include(u => u.Rentals)
            .ThenInclude(r => r.Car)
            .ToList();
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int UserId)
    {
        var user = _context.Users.Find(UserId);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

}