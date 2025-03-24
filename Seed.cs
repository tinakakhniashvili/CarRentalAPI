using System.Linq;
using CarRentalApp.Data;
using CarRentalApp.Interfaces;
using CarRentalApp.Models;

namespace CarRentalApp
{
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
            Console.WriteLine("Starting database seeding...");

            if (!_context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin" },
                    new Role { Name = "User" }
                };

                _context.Roles.AddRange(roles);
                _context.SaveChanges();
            }

            var adminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
            var userRole = _context.Roles.FirstOrDefault(r => r.Name == "User");

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
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    DateJoined = DateTime.UtcNow,
                    Roles = new List<Role> { adminRole }
                };

                var userPassword = "User@123";
                _authService.CreatePasswordHash(userPassword, out byte[] userPasswordHash, out byte[] userPasswordSalt);

                var regularUser = new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    PhoneNumber = "987654321",
                    PasswordHash = userPasswordHash,
                    PasswordSalt = userPasswordSalt,
                    DateJoined = DateTime.UtcNow,
                    Roles = new List<Role> { userRole }
                };

                _context.Users.AddRange(adminUser, regularUser);
                _context.SaveChanges();
            }

            if (!_context.Cars.Any())
            {
                var cars = new List<Car>
                {
                    new Car { Brand = "Toyota", Model = "Camry", Year = 2022, Description = "Comfortable sedan", PricePerDay = 50m, IsAvailable = true, ImageUrl = "https://example.com/camry.jpg" },
                    new Car { Brand = "Honda", Model = "Civic", Year = 2021, Description = "Reliable compact car", PricePerDay = 45m, IsAvailable = true, ImageUrl = "https://example.com/civic.jpg" },
                    new Car { Brand = "Ford", Model = "Focus", Year = 2023, Description = "Stylish and efficient", PricePerDay = 55m, IsAvailable = true, ImageUrl = "https://example.com/focus.jpg" }
                };

                _context.Cars.AddRange(cars);
                _context.SaveChanges();
            }

            if (!_context.Rentals.Any())
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == "johndoe@example.com");
                var car = _context.Cars.FirstOrDefault(c => c.Model == "Camry");

                if (user != null && car != null)
                {
                    var rental = new Rental
                    {
                        UserId = user.Id,
                        CarId = car.Id,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddDays(7),
                        TotalPrice = car.PricePerDay * 7,
                        RentalStatus = "Active"
                    };

                    _context.Rentals.Add(rental);
                    _context.SaveChanges();
                }
            }

            Console.WriteLine("Database seeding completed.");
        }
    }
}
