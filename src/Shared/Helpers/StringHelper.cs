namespace clean_architecture.Shared.Helpers;

public static class StringHelper
{
    public static string? Truncate(string? value, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return value.Length <= maxLength ? value : value[..maxLength];
    }
}