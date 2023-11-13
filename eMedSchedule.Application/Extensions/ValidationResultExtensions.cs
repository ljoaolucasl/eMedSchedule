using eMedSchedule.Application.Common;

namespace eMedSchedule.Application.Extensions
{
    public static class ValidationResultExtensions
    {
        public static List<IError> ConvertToErrorList(this ValidationResult validation)
        {
            return new List<IError>(validation.Errors.Select(item => new CustomError(item.ErrorMessage, item.PropertyName)));
        }
    }
}