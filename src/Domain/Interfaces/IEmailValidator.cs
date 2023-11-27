namespace clean_architecture.Domain.Interfaces;

public interface IEmailValidator
{
    bool IsValidEmail(string email);
}