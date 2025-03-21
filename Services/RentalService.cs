using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Interfaces.Repositories;
using CarRentalApp.Models;

namespace CarRentalApp.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    // private readonly ICarRepository _carRepository;

    public RentalService(IRentalRepository rentalRepository /*ICarRepository carRepository */)
    {
        _rentalRepository = rentalRepository;
        // _carRepository = carRepository;
    }
    public RentalDTO GetRentalById(int rentalId)
    {
        var rental = _rentalRepository.GetRentalById(rentalId);
        if (rental == null) return null;

        return new RentalDTO
        {
            Id = rental.Id,
            CarId = rental.CarId,
            UserId = rental.UserId,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            TotalPrice = rental.TotalPrice
        };
    }

    public List<RentalDTO> GetRentalByUserId(int userId)
    {
        var rentals = _rentalRepository.GetRentalsByUserId(userId);

        return rentals.Select(r => new RentalDTO
        {
            Id = r.Id,
            CarId = r.CarId,
            UserId = r.UserId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            TotalPrice = r.TotalPrice
        }).ToList();
    }

    public Rental CreateRental(CreateRentalDTO rentalDto)
    {
        var rental = new Rental
        {
            CarId = rentalDto.CarId,
            UserId = rentalDto.UserId,
            StartDate = rentalDto.StartDate,
            EndDate = rentalDto.EndDate,
            TotalPrice = rentalDto.TotalPrice
        };
        
        _rentalRepository.AddRental(rental);
        return rental;
    }

    public void DeleteRental(int rentalId)
    {
        _rentalRepository.DeleteRental(rentalId);
    }
}