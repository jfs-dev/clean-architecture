using Microsoft.EntityFrameworkCore;
using Moq;

using clean_architecture.Domain.Interfaces;
using clean_architecture.Domain.Models;
using clean_architecture.Infrastructure.Data;
using clean_architecture.Infrastructure.Repositories;

namespace clean_architecture.Tests.InfrastructureTestes;

[TestClass]
public class UserRepositoryTests
{
    private AppDbContext _dbContext = null!;
    private UserRepository _repository = null!;
    private readonly Mock<IEmailValidator> _mockEmailValidator = new();

    [TestInitialize]
    public void Initialize()
    {
        _dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("clean-architecture-tests").Options);
        _repository = new UserRepository(_dbContext, _mockEmailValidator.Object);

        _dbContext.Users.Add(new User(_mockEmailValidator.Object) { Name = "Peter Parker", Email = "peter.parker@marvel.com" });
        _dbContext.Users.Add(new User(_mockEmailValidator.Object) { Name = "Mary Jane", Email = "mary.jane@marvel.com" });
        
        _dbContext.SaveChanges();
    }

    [TestMethod]
    [TestCategory("CreateUser")]
    public void Create_User_Should_Add_User_To_Database()
    {
        var user = new User(_mockEmailValidator.Object) { Name = "Ben Parker", Email = "ben.parker@marvel.com" };

        var createdUser = _repository.CreateUser(user);

        Assert.IsNotNull(createdUser);
        Assert.AreEqual(user.Name, createdUser.Name);
        Assert.AreEqual(user.Email, createdUser.Email);
    }

    [TestMethod]
    [TestCategory("UpdateUser")]
    public void Update_User_Should_Update_User_In_Database()
    {
        var user = new User(_mockEmailValidator.Object) { Id = 2, Name = "Mary Jane Watson", Email = "mary.jane.watson@marvel.com" };

        var updatedUser = _repository.UpdateUser(user);

        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(user.Name, updatedUser.Name);
        Assert.AreEqual(user.Email, updatedUser.Email);
    }

    [TestMethod]
    [TestCategory("DeleteUser")]
    public void Delete_User_Should_Delete_User_From_Database()
    {
        var isDeleted = _repository.DeleteUser(2);

        Assert.IsTrue(isDeleted);
        Assert.IsNull(_dbContext.Users.Find(2));
    }

    [TestMethod]
    [TestCategory("GetUserById")]
    public void Get_User_By_Id_Should_Retrieve_User_From_Database()
    {
        var user = _repository.GetUserById(1);

        Assert.IsNotNull(user);
        Assert.AreEqual("Peter Parker", user.Name);
        Assert.AreEqual("peter.parker@marvel.com", user.Email);
    }

    [TestMethod]
    [TestCategory("GetAllUsers")]
    public void Get_All_Users_Should_Retrieve_All_Users_From_Database()
    {
        var users = _repository.GetAllUsers();

        Assert.IsNotNull(users);
        Assert.AreEqual(2, users.Count);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}