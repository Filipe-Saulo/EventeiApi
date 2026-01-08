using Api.Models.Dto.WebUserLogin;
using Api.Repositories.IRepository;
using Api.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _AccountRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository webUserAccount, ILogger<AccountController> logger)
        {
            _AccountRepository = webUserAccount;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string>>> Register(WebCreateUserLoginDto adminDto)
        {
            if (adminDto == null)
                return BadRequest(ApiResponse<string>.BadRequest(StandardMessages.NullBody));

            var result = await _AccountRepository.Register(adminDto);

            if (result.Any())
            {
                var errors = result.Select(e => $"{e.Code}: {e.Description}");
                return BadRequest(ApiResponse<string>.BadRequest(string.Join("; ", errors)));
            }

            return Ok(ApiResponse<string>.Ok(null, "Admin registrado com sucesso."));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            var authResponse = await _AccountRepository.Login(loginDto);

            if (authResponse == null)
            {
                _logger.LogWarning($"Admin with email {loginDto.Email} was not found or password is incorrect.");
                return Unauthorized(ApiResponse<AuthResponseDto>.Unauthorized("Email ou Senha inválidos"));
            }

            //USAR A VERSAO ABAIXO PARA PRODUÇÃO
            Response.Cookies.Append(
                "refreshToken",
                authResponse.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7),
                }
            );

            var responseWithoutRefreshToken = new AuthResponseDto
            {
                Token = authResponse.Token,
                UserId = authResponse.UserId
            };

            return Ok(ApiResponse<AuthResponseDto>.Ok(responseWithoutRefreshToken));
        }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshToken()
        {
            // 1. Lê o refreshToken do cookie
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized(ApiResponse<AuthResponseDto>.Unauthorized("Refresh Token não encontrado"));

            // 2. Lê o accessToken expirado do header Authorization
            var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(accessToken))
                return BadRequest(ApiResponse<AuthResponseDto>.BadRequest("Token inválido"));

            // 3. Verifica e gera novos tokens
            var result = await _AccountRepository.VerifyRefreshToken(new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });

            Response.Cookies.Append(
                "refreshToken",
                result.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7),
                }
            );

            // 5. Retorna apenas o novo accessToken no corpo
            return Ok(ApiResponse<AuthResponseDto>.Ok(new AuthResponseDto
            {
                Token = result.Token,
                UserId = result.UserId
            }));
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {

            Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });



            return Ok(ApiResponse<object>.Ok("Logout realizado com sucesso"));
        }
    }
}
