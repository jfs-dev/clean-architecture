using clean_architecture.Shared.Helpers;

namespace clean_architecture.Tests.SharedTests;

[TestClass]
public class StringHelperTests
{
    private readonly int _maxLength = 10;

    [TestMethod]
    [TestCategory("Truncate")]
    public void Truncate_ValidInput_Returns_Truncated_String()
    {
        string input = "Peter Parker";
        string expected = "Peter Park";

        string? truncated = StringHelper.Truncate(input, _maxLength);

        Assert.AreEqual(expected, truncated);
    }

    [TestMethod]
    [TestCategory("Truncate")]
    public void Truncate_ShortString_ReturnsSameString()
    {
        string input = "Mary Jane";
        string expected = "Mary Jane";

        string? truncated = StringHelper.Truncate(input, _maxLength);

        Assert.AreEqual(expected, truncated);
    }

    [TestMethod]
    [TestCategory("Truncate")]
    public void Truncate_EmptyString_ReturnsEmptyString()
    {
        string input = "";
        string expected = "";

        string? truncated = StringHelper.Truncate(input, _maxLength);

        Assert.AreEqual(expected, truncated);
    }

    [TestMethod]
    [TestCategory("Truncate")]
    public void Truncate_NullString_ReturnsNull()
    {
        string? input = null;

        string? truncated = StringHelper.Truncate(input, _maxLength);

        Assert.IsNull(truncated);
    }
}