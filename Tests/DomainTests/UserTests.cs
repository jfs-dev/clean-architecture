using Moq;

using clean_architecture.Domain.Interfaces;
using clean_architecture.Domain.Models;

namespace clean_architecture.Tests.DomainTests;

[TestClass]
public class UserTests
{
    private Mock<IEmailValidator> _mockEmailValidator = new();

    [TestMethod]
    [TestCategory("User")]
    public void User_Invalid_Name_Throws_Exception()
    {
        var user = new User(_mockEmailValidator.Object) { Name = "P" };

        Assert.ThrowsException<InvalidOperationException>(() => user.Validate());
    }

    [TestMethod]
    [TestCategory("User")]
    public void User_Invalid_Email_Throws_Exception()
    {
        _mockEmailValidator.Setup(emailValidator => emailValidator.IsValidEmail(It.IsAny<string>())).Returns(false);

        var user = new User(_mockEmailValidator.Object) { Name = "Peter Parker", Email = "peter.parker.com" };

        Assert.ThrowsException<InvalidOperationException>(() => user.Validate());
    }

    [TestMethod]
    [TestCategory("User")]
    public void User_Valid_Data_Passes_Validation()
    {
        _mockEmailValidator.Setup(emailValidator => emailValidator.IsValidEmail(It.IsAny<string>())).Returns(true);

        var user = new User(_mockEmailValidator.Object) { Name = "Peter Parker", Email = "peter.parker@marvel.com" };

        user.Validate();
        Assert.IsTrue(true);
    }
}