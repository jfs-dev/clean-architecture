using Moq;

using clean_architecture.Application.Interfaces;
using clean_architecture.Application.Services;
using clean_architecture.Domain.Models;
using clean_architecture.Domain.Interfaces;

namespace clean_architecture.Tests.ApplicationTests;

[TestClass]
public class UserServiceTests
{
    private UserService _userService = null!;
    private readonly Mock<IUserRepository> _mockRepository = new();
    private readonly Mock<IEmailValidator> _mockEmailValidator = new();

    [TestInitialize]
    public void Initialize()
    {
        _userService = new UserService(_mockRepository.Object);
        _mockEmailValidator.Setup(emailValidator => emailValidator.IsValidEmail(It.IsAny<string>())).Returns(true);
    }

    [TestMethod]
    [TestCategory("CreateUser")]
    public void Create_User_Valid_User_Returns_Created_User()
    {
        var user = new User(_mockEmailValidator.Object) { Name = "Peter Parker", Email = "peter.parker@marvel.com" };

        _mockRepository.Setup(repo => repo.CreateUser(user)).Returns(user);

        var createdUser = _userService.CreateUser(user);

        Assert.IsNotNull(createdUser);
        Assert.AreEqual(user, createdUser);
    }

    [TestMethod]
    [TestCategory("UpdateUser")]
    public void Update_User_Valid_User_Returns_Updated_User()
    {
        var user = new User(_mockEmailValidator.Object) { Id = 1, Name = "Peter Parker", Email = "peter.parker@marvel.com" };

        _mockRepository.Setup(repo => repo.UpdateUser(user)).Returns(user);

        var updatedUser = _userService.UpdateUser(user);

        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(user, updatedUser);
    }

    [TestMethod]
    [TestCategory("DeleteUser")]
    public void Delete_User_Existing_User_Id_Returns_True()
    {
        var userId = 1;

        _mockRepository.Setup(repo => repo.DeleteUser(userId)).Returns(true);

        var isDeleted = _userService.DeleteUser(userId);

        Assert.IsTrue(isDeleted);
    }

    [TestMethod]
    [TestCategory("GetUserById")]
    public void Get_User_By_Id_Valid_User_Id_Returns_User()
    {
        var user = new User(_mockEmailValidator.Object) { Id = 1, Name = "Peter Parker", Email = "peter.parker@marvel.com" };

        _mockRepository.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns(user);

        var returnedUser = _userService.GetUserById(1);

        Assert.IsNotNull(returnedUser);
        Assert.AreEqual(user, returnedUser);
    }

    [TestMethod]
    [TestCategory("GetAllUsers")]
    public void Get_All_Users_Returns_List_Of_Users()
    {
        var users = new List<User>
        {
            new(_mockEmailValidator.Object) { Id = 1, Name = "Peter Parker", Email = "peter.parker@marvel.com" },
            new(_mockEmailValidator.Object) { Id = 2, Name = "Ben Parker", Email = "ben.parker@marvel.com" },
            new(_mockEmailValidator.Object) { Id = 2, Name = "Mary Jane", Email = "mary.jane@marvel.com" }
        };

        _mockRepository.Setup(repo => repo.GetAllUsers()).Returns(users);

        var returnedUsers = _userService.GetAllUsers();

        Assert.IsNotNull(returnedUsers);
        CollectionAssert.AreEqual(users, returnedUsers);
    }
}