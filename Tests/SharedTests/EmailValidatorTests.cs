using clean_architecture.Shared.Validators;

namespace clean_architecture.Tests.SharedTests;

[TestClass]
public class EmailValidatorTests
{
    private readonly EmailValidator _emailValidator = new();

    [TestMethod]
    [TestCategory("IsValidEmail")]
    public void IsValidEmail_ValidEmail_Returns_True()
    {
        var validEmail = "peter.parker@marvel.com";

        var isValid = _emailValidator.IsValidEmail(validEmail);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory("IsValidEmail")]
    public void IsValidEmail_InvalidEmail_Returns_False()
    {
        var invalidEmail = "peter.parker.com";

        var isValid = _emailValidator.IsValidEmail(invalidEmail);

        Assert.IsFalse(isValid);
    }
}