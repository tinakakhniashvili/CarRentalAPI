using CarRentalApp.DTOs;

namespace CarRentalApp.Interfaces;

public interface ICarService
{
    IEnumerable<CarDTO> GetAllCars();
    CarDTO GetCarById(int carId);
    // CarDTO CreateCar(CarCreateDTO carCreateDto);
    // CarDTO UpdateCar(int carId, CarUpdateDtO carUpdateDTO);
    void DeleteCar(int carId);
}