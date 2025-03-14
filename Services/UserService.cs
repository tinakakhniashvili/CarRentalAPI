using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Interfaces.Repositories;

namespace CarRentalApp.Services;

public class UserService // : IUserService
{
    /*
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public UserDTO GetUserById(int userId)
    {
        var user = _userRepository.GetUserById(userId);
        if (user == null) return null;

        return new UserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };
    }

    public UserDTO UpdateUser(int userId, UserDTO userDTO)
    {
        var user = _userRepository.GetUserById(userId);
        if (user == null) return null;

        user.FullName = userDTO.FullName;
        user.Email = userDTO.Email;
        
        _userRepository.UpdateUser(user);

        return new UserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };
    } */
}