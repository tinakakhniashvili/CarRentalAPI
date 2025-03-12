using CarRentalApp.Models;

namespace CarRentalApp.Interfaces.Repositories;

public interface IRentalRepository
{
    Rental GetRentalById(int rentalId);
    List<Rental> GetRentalsByUserId(int userId);
    void AddRental(Rental rental);
    void UpdateRental(Rental rental);
    void DeleteRental(int rentalId);
}