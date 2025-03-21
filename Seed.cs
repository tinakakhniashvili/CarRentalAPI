using CarRentalApp.Data;
using CarRentalApp.Interfaces;
using CarRentalApp.Models;

namespace CarRentalApp;

public class Seed
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthService _authService;

    public Seed(ApplicationDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public void SeedDataContext()
    {
        if (!_context.Users.Any())
        {
            var adminPassword = "Admin@123";
            _authService.CreatePasswordHash(adminPassword, out byte[] passwordHash, out byte[] passwordSalt);

            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                PhoneNumber = "123456789",
                Role = "Admin",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DateJoined = DateTime.UtcNow
            };

            _context.Users.Add(adminUser);
            _context.SaveChanges();
        }
    }
}