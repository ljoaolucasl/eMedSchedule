using FluentValidation.Results;

namespace eMedSchedule.Domain.Common
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T instance);
    }
}