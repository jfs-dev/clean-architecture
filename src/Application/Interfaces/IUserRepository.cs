using clean_architecture.Domain.Models;

namespace clean_architecture.Application.Interfaces;

public interface IUserRepository
{
    User CreateUser(User user);
    User UpdateUser(User user);
    bool DeleteUser(int userId);
    User GetUserById(int id);
    List<User> GetAllUsers();
}