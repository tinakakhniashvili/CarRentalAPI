using CarRentalApp.Models;

namespace CarRentalApp.Interfaces.Repositories;

public interface IUserRepository
{
    User GetUserByEmail(string email);
    User GetUserById(int userId);
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int UserId);
    List<User> GetAllUsers();
}