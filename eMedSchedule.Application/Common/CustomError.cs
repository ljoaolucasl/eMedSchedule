namespace eMedSchedule.Application.Common
{
    public class CustomError : Error
    {
        public string ErrorMessage { get; set; }

        public string PropertyName { get; set; }

        public string ExceptionMessage { get; set; }

        public CustomError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public CustomError(string errorMessage, string propertyName) : this(errorMessage)
        {
            PropertyName = propertyName;
        }

        public CustomError(string errorMessage, string propertyName, string exceptionMessage) : this(errorMessage, propertyName)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
}