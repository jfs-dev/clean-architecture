using System.Text.RegularExpressions;

using clean_architecture.Domain.Interfaces;

namespace clean_architecture.Shared.Validators;

public partial class EmailValidator : IEmailValidator
{
    [GeneratedRegex($@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
    private static partial Regex EmailGeneratedRegex();

    public bool IsValidEmail(string email)
    {
        return EmailGeneratedRegex().IsMatch(email);
    }
}