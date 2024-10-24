using BookStore.Application.Common.Dto.Auth;
using FluentValidation;

namespace BookStore.Application.Common.Validators.AuthValidators.LoginValidators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty().WithMessage("Email or username is required.")
            .Must(BeAValidEmailOrUsername).WithMessage("Invalid email or username format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }

    private bool BeAValidEmailOrUsername(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }
        
        if (value.Contains("@"))
        {
            var parts = value.Split('@');
            return parts.Length == 2 && 
                   !string.IsNullOrWhiteSpace(parts[0]) && // pre @ ne sme biti prazan
                   parts[1].Contains('.') && // posle @ mora sadržati tačku
                   !string.IsNullOrWhiteSpace(parts[1]); // posle @ ne sme biti prazan
        }
        
        return value.Length >= 6 && value.Length <= 20; 
    }
}