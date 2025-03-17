using CarRentalApp.DTOs;
using CarRentalApp.Models;

namespace CarRentalApp.Interfaces;

public interface IRentalService
{
     RentalDTO GetRentalById(int rentalId);
     List<RentalDTO> GetRentalByUserId(int userId);
     Rental CreateRental(CreateRentalDTO rentalDto);
     void DeleteRental(int rentalId);
}