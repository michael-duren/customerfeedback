using CustomerFeedback.Models.DTOs;
using FluentValidation;

namespace CustomerFeedback.Endpoints.Account
{
    public class AccountValidator : AbstractValidator<RegisterDto>
    {
        private readonly string _regex;

        public AccountValidator()
        {
            _regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Username must have at least 6 characters");
            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Display name bust at least 3 characters long");
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Please enter a non empty email address");
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(_regex)
                .WithMessage(
                    "Password must contain an uppercase and lowercase letter, a special character, a number, and must be 8 characters or more"
                );
        }
    }
}
