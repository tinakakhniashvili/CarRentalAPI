using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CarRentalApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public UserDTO GetUserById(int userId)
    {
        var user = _userRepository.GetUserById(userId);

        if (user == null) return null;

        var userDTO = new UserDTO
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Rentals = user.Rentals
                .Select(rental => new RentalDTO
                {
                    Id = rental.Id,
                    CarId = rental.CarId,
                })
                .ToList() 
        };

        return userDTO;
    }

    public UserDTO UpdateUser(int userId, UserDTO userDTO)
    {
        var user = _userRepository.GetUserById(userId);
        if(user ==null) return null;

        user.FirstName = userDTO.FirstName;
        user.LastName = userDTO.LastName;
        user.Email = userDTO.Email;
        user.PhoneNumber = userDTO.PhoneNumber;
        
        _userRepository.UpdateUser(user);

        return userDTO;
    }

    public List<UserDTO> GetAllUsers()
    {
        var users = _userRepository.GetAllUsers(); 
        
        var userDTOs = users.Select(u => new UserDTO
        {
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            Rentals = u.Rentals 
                ?.Select(rental => new RentalDTO
                {
                    Id = rental.Id,
                    CarId = rental.CarId,
                })
                .ToList() ?? new List<RentalDTO>() 
        }).ToList();

        return userDTOs;
    }

    public void DeleteUser(int userId)
    {
        // var user = _userRepository.GetUserById(userId);
        // if (user == null)
        //     return NotFound(new { message = "Rental not found" });
        
        _userRepository.DeleteUser(userId);
    }
}