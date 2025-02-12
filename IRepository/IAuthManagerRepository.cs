using Api.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace Api.IRepository
{
    public interface IAuthManagerRepository
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        Task<AuthResponseDto> Login(LoginUserDto loginUserDto);
        Task<string> CreateRefreshToken();
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
