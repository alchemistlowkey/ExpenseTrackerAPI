using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using Shared.DataTransferObjects;
using Serilog;

namespace ExpenseTrackerApi.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IServiceManager service, ILogger<AuthenticationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                Log.Error($"Registration failed for user {userForRegistration.Email}");
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            Log.Information($"Registration succeeded for user {userForRegistration.Email}");
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            try
            {
                if (!await _service.AuthenticationService.ValidateUser(user))
                {
                    Log.Information($"Authentication failed for user {user.UserName}");
                    return Unauthorized();
                }
                var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);
                Log.Information($"Authentication succeeded for user {user.UserName}");
                return Ok(tokenDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Login failed for user {UserName}", user.UserName);
                return StatusCode(500, ex.Message); // temporary - remove before final deploy
            }
        }
    }
}
