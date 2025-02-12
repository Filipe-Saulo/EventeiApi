using Api.IRepository;
using Api.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManagerRepository _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManagerRepository authManager, ILogger<AccountController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register([FromBody] ApiUserDto apiUserDto)
        {
            _logger.LogInformation($"Registration Attempt for {apiUserDto.Email}");
            var errors = await _authManager.Register(apiUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            _logger.LogInformation($"Login Attempt for {loginUserDto.Email} ");
            var authResponse = await _authManager.Login(loginUserDto);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);

        }

        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }



    }
}
