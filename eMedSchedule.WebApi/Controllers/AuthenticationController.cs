using eMedSchedule.Domain.AuthenticationModule;
using eMedSchedule.WebApi.ViewModels.AuthenticationModule;

namespace eMedSchedule.WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper) : base(mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            var user = _mapper.Map<User>(registerUserViewModel);

            var userResult = await _authenticationService.RegisterAsync(user, registerUserViewModel.Password);

            if (userResult.IsFailed)
                return BadRequest(userResult.Errors);

            var tokenViewModel = user.GenerateJwt(DateTime.Now.AddDays(5));

            return Ok(tokenViewModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(AuthenticateUserViewModel authenticateUserViewModel)
        {
            var userResult = await _authenticationService.Authenticate(authenticateUserViewModel.Email, authenticateUserViewModel.Password);

            if (userResult.IsFailed)
                return BadRequest(userResult.Errors);

            var tokenViewModel = userResult.Value.GenerateJwt(DateTime.Now.AddDays(5));

            return Ok(tokenViewModel);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();

            return Ok();
        }
    }
}