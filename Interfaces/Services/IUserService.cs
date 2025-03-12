using CarRentalApp.DTOs;

namespace CarRentalApp.Interfaces;

public interface IUserService
{
    UserDTO GetUserById(int userId);
    UserDTO UpdateUser(int userId, UserDTO userDTO);
}