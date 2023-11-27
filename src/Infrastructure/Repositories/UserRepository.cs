using clean_architecture.Application.Interfaces;
using clean_architecture.Domain.Interfaces;
using clean_architecture.Domain.Models;
using clean_architecture.Infrastructure.Data;

namespace clean_architecture.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext, IEmailValidator emailValidator) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IEmailValidator _emailValidator = emailValidator;

    public User CreateUser(User user)
    {
        _dbContext.Add(user);
        _dbContext.SaveChanges();

        return user;
    }

    public User UpdateUser(User user)
    {
        var existingUser = _dbContext.Users.Find(user.Id) ?? throw new InvalidOperationException("Usuário não encontrado!");
        
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        
        _dbContext.SaveChanges();

        return existingUser;
    }

    public bool DeleteUser(int userId)
    {
        var user = _dbContext.Users.Find(userId) ?? throw new InvalidOperationException("Usuário não encontrado!");

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        return true;
    }

    public User GetUserById(int id)
    {
        return _dbContext.Users.FirstOrDefault(x => x.Id == id) ?? new(_emailValidator);
    }

    public List<User> GetAllUsers()
    {
        return [.. _dbContext.Users];
    }
}