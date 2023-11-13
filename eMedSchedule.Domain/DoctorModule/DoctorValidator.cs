﻿using FluentValidation;
using System.Text.RegularExpressions;

namespace eMedSchedule.Domain.DoctorModule
{
    public class DoctorValidator : AbstractValidator<Doctor>, IDoctorValidator
    {
        public DoctorValidator()
        {
            RuleFor(d => d.Name)
                .NotNull()
                .MinimumLength(3).WithMessage(@"'Name' must be greater than or equal to 3 characters.")
                .Custom(ValidateInvalidCharacter)
                .NotEmpty();

            RuleFor(d => d.CRM)
            .NotEmpty()
            .NotNull()
            .Must(ValidateCRM).WithMessage("'Invalid 'CRM'.");
        }

        private void ValidateInvalidCharacter(string name, ValidationContext<Doctor> context)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}'\s-\d]+$"))
                context.AddFailure("Invalid Character");
        }

        private bool ValidateCRM(string crm)
        {
            if (string.IsNullOrWhiteSpace(crm))
                return false;

            Regex crmRegex = new(@"^\d{5}-[A-Za-z]{2}$");

            return crmRegex.IsMatch(crm);
        }
    }
}