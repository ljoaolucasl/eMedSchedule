using FluentValidation;
using System.Text.RegularExpressions;

namespace eMedSchedule.Domain.DoctorActivityModule
{
    public class DoctorActivityValidator : AbstractValidator<DoctorActivity>, IDoctorActivityValidator
    {
        public DoctorActivityValidator()
        {
            RuleFor(dA => dA.Title)
                .NotNull().WithMessage("'Title' is required.")
                .MinimumLength(3).WithMessage(@"'Title' must be greater than or equal to 3 characters.")
                .Custom(ValidateInvalidCharacter)
                .NotEmpty().WithMessage("'Title' cannot be empty.");

            RuleFor(dA => dA.Doctors)
                .Must(d => d != null && d.Count > 0).WithMessage("'Doctor' is required.")
                .NotEmpty().WithMessage("'Doctors' cannot be empty.");

            RuleFor(dA => dA.ActivityType)
                .NotNull().WithMessage("'Activity' is required.")
                .IsInEnum().WithMessage("'Invalid 'Activity'.");

            RuleFor(dA => dA.Date)
                .NotNull().WithMessage("'Date' is required.");

            RuleFor(dA => dA)
                .Must(dA => dA.Doctors != null ? ValidateDoctorsByTypeActivity(dA) : true).WithMessage("An appointment cannot have more than 1 doctor.")
                .Must(dA => dA.Doctors != null ? dA.Doctors.All(d => d.ValidateDoctorSchedule(dA)) : true).WithMessage("Doctor has a scheduling conflict at this time.");
        }

        private void ValidateInvalidCharacter(string name, ValidationContext<DoctorActivity> context)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}'\s-\d]+$"))
                context.AddFailure("Invalid Character");
        }

        private bool ValidateDoctorsByTypeActivity(DoctorActivity activity)
        {
            return !(activity.ActivityType == ActivityTypeEnum.Appointment && activity.Doctors.Count > 1);
        }
    }
}