using Api.Configurations;
using Api.Models.Dto.WebUserLogin;
using Api.Repositories.IRepository;
using AutoMapper;
using Eventei_Api.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Repositories.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountRepository> _logger;
        private readonly DatabaseContext _context;

        private User _user;

        private const string _loginProvider = "BikeFacilApi";
        private const string _refreshToken = "RefreshToken";

        public AccountRepository(
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration,
            ILogger<AccountRepository> logger,
            DatabaseContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<IdentityError>> Register(WebCreateUserLoginDto adminDto)
        {
            var errors = new List<IdentityError>();

            
            var emailExists = await _context.Users.AnyAsync(u => u.Email == adminDto.Email);
                              

            if (emailExists)
            {
                errors.Add(new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = "E-mail já cadastrado no sistema."
                });
            }

            
            var cpfExists = await _context.Users.AnyAsync(u => u.Cpf == adminDto.Cpf);
                            

            if (cpfExists)
            {
                errors.Add(new IdentityError
                {
                    Code = "DuplicateCpf",
                    Description = "CPF já cadastrado no sistema."
                });
            }
            
            var phoneExists = await _context.Users.AnyAsync(u => u.PhoneNumber == adminDto.PhoneNumber);
                              

            if (phoneExists)
            {
                errors.Add(new IdentityError
                {
                    Code = "DuplicatePhoneNumber",
                    Description = "Número de celular já cadastrado no sistema."
                });
            }



            //Verifica se o Email é válido
            //if (!EmailValidator.Validar(adminDto.Email))
            //    errors.Add(new IdentityError
            //    {
            //        Code = "InvalidEmail",
            //        Description = "Email inválido."
            //    });

            //Verifica se o CPF cadastrado é válido
            //if (!CpfValidator.Validar(adminDto.Cpf))
            //{
            //    errors.Add(new IdentityError
            //    {
            //        Code = "InvalidCpf",
            //        Description = "CPF inválido."
            //    });
            //    return errors;
            //}
            //Verifica se o Celular é valido
            //if (!PhoneValidator.ValidarCelularBr(adminDto.PhoneNumber, out var numeroFormatado))
            //{
            //    errors.Add(new IdentityError
            //    {
            //        Code = "InvalidPhone",
            //        Description = "Número de celular inválido."
            //    });
            //}


            if (errors.Any())
                return errors;
            
            // Cria o usuário
            _user = new User
            {
                FullName = adminDto.FullName,                
                UserName = adminDto.Email,
                Email = adminDto.Email,
                PhoneNumber = adminDto.PhoneNumber,
                Cpf = adminDto.Cpf,
                RegisteredAt = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(_user, adminDto.Password);

            if (result.Succeeded)
            {
                try
                {
                    await _userManager.AddToRoleAsync(_user, "CompanyAdmin");
                }
                catch (Exception)
                {
                    await _userManager.DeleteAsync(_user);
                    throw;
                }
            }

            return result.Errors;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return null;

            var isAdmin = await _userManager.IsInRoleAsync(user, "CompanyAdmin");
            if (!isAdmin)
                return null;

            user.LastLogin = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            _user = user;
            var token = await GenerateToken();

            var refreshToken = await CreateRefreshToken();

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenContent = handler.ReadJwtToken(request.Token);
                var tokenExp = tokenContent.ValidTo;
                
                if (DateTime.UtcNow > tokenExp.AddMinutes(5))
                    throw new SecurityTokenException("Token expirado há muito tempo, faça login novamente");

                var email = tokenContent.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
                if (string.IsNullOrEmpty(email))
                    throw new SecurityTokenException("Token inválido");

                var userId = tokenContent.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                    throw new SecurityTokenException("Token sem identificador de usuário");

                _user = await _userManager.FindByEmailAsync(email);
                if (_user == null || _user.Id != userId)
                    throw new UnauthorizedAccessException("Usuário não encontrado ou não autorizado");

                var isValid = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);
                if (!isValid)
                {
                    await _userManager.UpdateSecurityStampAsync(_user);
                    throw new SecurityTokenException("Refresh token inválido");
                }

                var newAccessToken = await GenerateToken();
                var newRefreshToken = await CreateRefreshToken();

                return new AuthResponseDto
                {
                    Token = newAccessToken,
                    UserId = _user.Id,
                    RefreshToken = newRefreshToken
                };
            }
            catch
            {
                throw; 
            }
        }

        private async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newToken);
            return newToken;
        }

        private async Task<string> GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id)
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
