using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Models;

namespace CarRentalApp.Services;

public class RentalService : IRentalService
{
    public RentalDTO GetRentalById(int rentalId)
    {
        throw new NotImplementedException();
    }

    public List<RentalDTO> GetRentalByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public Rental CreateRental(CreateRentalDTO rentalDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteRental(int rentalId)
    {
        throw new NotImplementedException();
    }
}