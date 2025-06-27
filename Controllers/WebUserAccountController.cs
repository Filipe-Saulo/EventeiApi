using Api.Models.Dto.WebUserLogin;
using Api.Repositories.IRepository;
using Api.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    public class WebUserAccountController : ControllerBase
    {
        private readonly IWebUserAccountRepository _WebUserAccountRepository;
        private readonly ILogger<WebUserAccountController> _logger;

        public WebUserAccountController(IWebUserAccountRepository webUserAccount, ILogger<WebUserAccountController> logger)
        {
            _WebUserAccountRepository = webUserAccount;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string>>> Register(WebCreateUserLoginDto adminDto)
        {
            if (adminDto == null)
                return BadRequest(ApiResponse<string>.BadRequest(StandardMessages.NullBody));

            try
            {
                var result = await _WebUserAccountRepository.Register(adminDto);

                if (result.Any())
                {
                    //Email duplicado
                    var emailError = result.FirstOrDefault(e => e.Code == "DuplicateEmail");
                    if (emailError != null)
                        return BadRequest(ApiResponse<string>.BadRequest(emailError.Description));

                    //Erro de CPF duplicado
                    var cpfError = result.FirstOrDefault(e => e.Code == "DuplicateCpf");
                    if (cpfError != null)
                        return BadRequest(ApiResponse<string>.BadRequest(cpfError.Description));

                    //Erro de celular duplicado
                    var phoneNumberError = result.FirstOrDefault(e => e.Code == "DuplicatePhoneNumber");
                    if (phoneNumberError != null)
                        return BadRequest(ApiResponse<string>.BadRequest(phoneNumberError.Description));

                    //Erro de CPF com 2 registro na tabela user(poderá existir no max 2 registro, user e admin)
                    var maxCpfError = result.FirstOrDefault(e => e.Code == "MaxCpfLimitReached");
                    if (maxCpfError != null)
                        return BadRequest(ApiResponse<string>.BadRequest(maxCpfError.Description));

                    //Erro de celular com 2 registro na tabela user(poderá existir no max 2 registro, user e admin)
                    var maxPhoneError = result.FirstOrDefault(e => e.Code == "MaxPhoneLimitReached");
                    if (maxPhoneError != null)
                        return BadRequest(ApiResponse<string>.BadRequest(maxPhoneError.Description));

                    //Erro de e-mail com 2 registros na tabela user
                    var maxEmailError = result.FirstOrDefault(e => e.Code == "MaxEmailLimitReached");
                    if (maxEmailError != null)
                        return BadRequest(ApiResponse<string>.BadRequest(maxEmailError.Description));

                    var errorMessages = result.Select(e => $"{e.Code}: {e.Description}");
                    return BadRequest(ApiResponse<string>.BadRequest(string.Join("; ", errorMessages)));
                }

                return Ok(ApiResponse<string>.Ok(null, "Admin registrado com sucesso."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar admin");
                return StatusCode(500, ApiResponse<string>.InternalServerError());
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation($"Login attempt for admin {loginDto.Email}");

            try
            {
                var authResponse = await _WebUserAccountRepository.Login(loginDto);

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

                _logger.LogInformation($"Admin logged in successfully: {loginDto.Email}");
                return Ok(ApiResponse<AuthResponseDto>.Ok(responseWithoutRefreshToken));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error logging in admin {loginDto.Email}: {ex.Message}");
                return StatusCode(500, ApiResponse<AuthResponseDto>.InternalServerError("Internal server error."));
            }
        }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshToken()
        {
            try
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
                var result = await _WebUserAccountRepository.VerifyRefreshToken(new AuthResponseDto
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
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning(ex, "Token JWT inválido");
                Response.Cookies.Delete("refreshToken");
                return Unauthorized(ApiResponse<AuthResponseDto>.Unauthorized("Token inválido"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao renovar token");
                return StatusCode(500, ApiResponse<AuthResponseDto>.InternalServerError());
            }
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
