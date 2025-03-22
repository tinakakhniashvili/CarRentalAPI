using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Interfaces.Repositories;

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
            PhoneNumber = user.PhoneNumber
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
}