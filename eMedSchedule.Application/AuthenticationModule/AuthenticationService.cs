using eMedSchedule.Domain.AuthenticationModule;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace eMedSchedule.Application.AuthenticationModule
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserValidator _userValidator;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(UserManager<User> userManager, IUserValidator userValidator, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _userValidator = userValidator;
            _signInManager = signInManager;
        }

        public async Task<Result<User>> RegisterAsync(User user, string password)
        {
            Result result = ValidateService(user);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            IdentityResult userResult = await _userManager.CreateAsync(user, password);

            if (!userResult.Succeeded)
                return Result.Fail(userResult.Errors
                    .Select(error => new Error(error.Description)));

            return Result.Ok(user);
        }

        public async Task<Result<User>> Authenticate(string email, string password)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(email, password, false, true);

            var errors = new List<Error>();

            if (loginResult.IsLockedOut)
                errors.Add(new Error("Access to this user has been blocked"));

            if (loginResult.IsNotAllowed)
                errors.Add(new Error("Login or password invalid"));

            if (!loginResult.Succeeded)
                errors.Add(new Error("Login or password invalid"));

            if (errors.Count > 0)
                return Result.Fail(errors);

            var user = await _userManager.FindByNameAsync(email);

            return Result.Ok(user);
        }

        public async Task<Result<User>> Logout()
        {
            await _signInManager.SignOutAsync();

            return Result.Ok();
        }

        public Result ValidateService(User obj)
        {
            var resultValidation = _userValidator.Validate(obj);

            var errors = new List<Error>();

            foreach (var validationFailure in resultValidation.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                errors.Add(new Error(validationFailure.ErrorMessage));
            }

            if (errors.Any())
                return Result.Fail(errors);

            return Result.Ok();
        }
    }
}