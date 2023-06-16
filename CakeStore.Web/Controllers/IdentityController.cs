using CakeStore.BL.Interfaces;
using CakeStore.Models.Request.Identity;
using CakeStore.Models.Requests.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CakeStore.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(
            IIdentityService identity,
            IOptions<AppSettings> appSettings,
            ILogger<IdentityController> logger)
        {
            _identityService = identity;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
        {
            try
            {
                var user = await _identityService.FindByUserName(request.UserName);

                if (user is null)
                {
                    return Unauthorized();
                }

                var isPasswordValid = user.Password == request.Password;

                if (!isPasswordValid)
                {
                    return Unauthorized();
                }

                var token = _identityService.GenerateJwtToken(user.Id.ToString(), request.UserName, _appSettings.Secret);

                return new LoginResponseModel
                {
                    Token = token
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(Add),
                    nameof(CakeController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(Add))]
        public async Task<IActionResult> Add(AddUserRequest addUserRequest)
        {
            try
            {
                var user = await _identityService.FindByUserName(addUserRequest.Username);

                if (user is not null)
                {
                    return BadRequest("There is already a user with that username. Please try with another one");
                }

                await _identityService.Add(addUserRequest);

                return Created(nameof(this.Add), new { UserId = user!.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ControllerName}: {ErrorMessage}",
                    nameof(Add),
                    nameof(CakeController),
                    ex.Message);

                return StatusCode(500, "An error occurred");
            }
        }
    }
}
