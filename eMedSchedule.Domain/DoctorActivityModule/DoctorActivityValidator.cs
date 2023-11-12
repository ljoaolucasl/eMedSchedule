using FluentValidation;
using System.Text.RegularExpressions;

namespace eMedSchedule.Domain.DoctorActivityModule
{
    public class DoctorActivityValidator : AbstractValidator<DoctorActivity>, IDoctorActivityValidator
    {
        public DoctorActivityValidator()
        {
            RuleFor(dA => dA.Title)
                .MinimumLength(3).WithMessage(@"'Title' must be greater than or equal to 3 characters.")
                .Custom(ValidateInvalidCharacter)
                .NotEmpty();

            RuleFor(dA => dA.Doctors)
                .NotNull().WithMessage("'Doctor' is required.");

            RuleFor(dA => dA.ActivityType)
                .NotEmpty().WithMessage("'Activity' cannot be empty.")
                .IsInEnum().WithMessage("'Invalid 'Activity'.");

            RuleFor(dA => dA.StartTime)
                .LessThan(a => a.EndTime).WithMessage("'Start Time' must be less than the 'End Time'.");

            RuleFor(dA => dA.EndTime)
                .GreaterThan(a => a.StartTime).WithMessage("'End Time' must be greater than 'Start Time'.");
        }

        private void ValidateInvalidCharacter(string name, ValidationContext<DoctorActivity> context)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}'\s-\d]+$"))
                context.AddFailure("Invalid Character");
        }
    }
}