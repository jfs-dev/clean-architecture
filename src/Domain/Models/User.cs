using clean_architecture.Domain.Interfaces;

namespace clean_architecture.Domain.Models;

public class User
{
    private readonly IEmailValidator _emailValidator = null!;

    private User() { }

    public User(IEmailValidator emailValidator) => _emailValidator = emailValidator;

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name) || Name.Length < 3)
            throw new InvalidOperationException("O nome deve ter pelo menos 3 caracteres!");
        
        if (!_emailValidator.IsValidEmail(Email))
            throw new InvalidOperationException("E-mail inválido!");
    }
}