using FluentValidation;
using System.Text.RegularExpressions;

namespace eMedSchedule.Domain.AuthenticationModule
{
    public class UserValidator : AbstractValidator<User>, IUserValidator
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotNull()
                .MinimumLength(3).WithMessage(@"'Name' must be greater than or equal to 3 characters.")
                .Custom(ValidateInvalidCharacter)
                .NotEmpty();
        }

        private void ValidateInvalidCharacter(string name, ValidationContext<User> context)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}'\s-\d]+$"))
                context.AddFailure("Invalid Character");
        }
    }
}