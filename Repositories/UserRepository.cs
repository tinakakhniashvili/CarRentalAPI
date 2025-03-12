using CarRentalApp.Interfaces.Repositories;
using CarRentalApp.Models;

namespace CarRentalApp.Repositories;

public class UserRepository : IUserRepository
{
    public User GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public User GetUserById(int userId)
    {
        throw new NotImplementedException();
    }

    public void AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int UserId)
    {
        throw new NotImplementedException();
    }
}