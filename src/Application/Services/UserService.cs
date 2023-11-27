using clean_architecture.Application.Interfaces;
using clean_architecture.Domain.Models;

namespace clean_architecture.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public User CreateUser(User user)
    {
        user.Validate();

        return _userRepository.CreateUser(user);
    }

    public User UpdateUser(User user)
    {
        user.Validate();

        return _userRepository.UpdateUser(user);
    }

    public bool DeleteUser(int userId)
    {
        return _userRepository.DeleteUser(userId);
    }

    public User GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }

    public List<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }
}