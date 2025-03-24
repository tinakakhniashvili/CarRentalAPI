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
                    Role = "Admin"
                };

                var userPassword = "User@123";
                _authService.CreatePasswordHash(userPassword, out byte[] userPasswordHash, out byte[] userPasswordSalt);

                var regularUser1 = new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    PhoneNumber = "987654321",
                    PasswordHash = userPasswordHash,
                    PasswordSalt = userPasswordSalt,
                    DateJoined = DateTime.UtcNow,
                    Role = "User"
                };

                var regularUser2 = new User
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "janedoe@example.com",
                    PhoneNumber = "123987456",
                    PasswordHash = userPasswordHash,
                    PasswordSalt = userPasswordSalt,
                    DateJoined = DateTime.UtcNow,
                    Role = "User"
                };

                var regularUser3 = new User
                {
                    FirstName = "Mark",
                    LastName = "Smith",
                    Email = "marksmith@example.com",
                    PhoneNumber = "555123456",
                    PasswordHash = userPasswordHash,
                    PasswordSalt = userPasswordSalt,
                    DateJoined = DateTime.UtcNow,
                    Role = "User"
                };

                _context.Users.AddRange(adminUser, regularUser1, regularUser2, regularUser3);
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
                var user1 = _context.Users.FirstOrDefault(u => u.Email == "johndoe@example.com");
                var user2 = _context.Users.FirstOrDefault(u => u.Email == "admin@example.com");
                var car1 = _context.Cars.FirstOrDefault(c => c.Model == "Camry");
                var car2 = _context.Cars.FirstOrDefault(c => c.Model == "Civic");
                var car3 = _context.Cars.FirstOrDefault(c => c.Model == "Focus");

                if (user1 != null && car1 != null)
                {
                    var rental1 = new Rental
                    {
                        UserId = user1.Id,
                        CarId = car1.Id,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddDays(5),
                        TotalPrice = car1.PricePerDay * 5,
                        RentalStatus = "Active"
                    };
                    _context.Rentals.Add(rental1);
                }

                if (user2 != null && car2 != null)
                {
                    var rental2 = new Rental
                    {
                        UserId = user2.Id,
                        CarId = car2.Id,
                        StartDate = DateTime.UtcNow.AddDays(2),
                        EndDate = DateTime.UtcNow.AddDays(6),
                        TotalPrice = car2.PricePerDay * 6,
                        RentalStatus = "Active"
                    };
                    _context.Rentals.Add(rental2);
                }

                if (user1 != null && car3 != null)
                {
                    var rental3 = new Rental
                    {
                        UserId = user1.Id,
                        CarId = car3.Id,
                        StartDate = DateTime.UtcNow.AddDays(3),
                        EndDate = DateTime.UtcNow.AddDays(7),
                        TotalPrice = car3.PricePerDay * 7,
                        RentalStatus = "Active"
                    };
                    _context.Rentals.Add(rental3);
                }

                _context.SaveChanges();
            }

            Console.WriteLine("Database seeding completed.");
        }
    }
}
