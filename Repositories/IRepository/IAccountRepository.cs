using Api.Models.Dto.WebUserLogin;
using Microsoft.AspNetCore.Identity;

namespace Api.Repositories.IRepository
{
    public interface IAccountRepository
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<IEnumerable<IdentityError>> Register(WebCreateUserLoginDto adminDto);
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
