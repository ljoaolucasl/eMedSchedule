using FluentResults;

namespace eMedSchedule.Domain.AuthenticationModule
{
    public interface IAuthenticationService
    {
        Task<Result<User>> RegisterAsync(User user, string password);

        Task<Result<User>> Authenticate(string email, string password);

        Task<Result<User>> Logout();

        Result ValidateService(User objectToValidate);
    }
}