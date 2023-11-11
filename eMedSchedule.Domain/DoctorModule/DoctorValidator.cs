using FluentValidation;

namespace eMedSchedule.Domain.DoctorModule
{
    public class DoctorValidator : AbstractValidator<Doctor>, IDoctorValidator
    {
    }
}