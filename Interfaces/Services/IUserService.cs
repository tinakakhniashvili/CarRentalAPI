using CarRentalApp.DTOs;
using CarRentalApp.Models;

namespace CarRentalApp.Interfaces;

public interface IUserService
{
    UserDTO GetUserById(int userId);
    UserDTO UpdateUser(int userId, UserDTO userDTO);
    List<UserDTO> GetAllUsers();
}