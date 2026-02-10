using Identity.API.Application.DTOs;
using Identity.API.Models;
using System.Security.Claims;

namespace Identity.API.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequest input);
        Task RegisterAsync(RegisterRequest input);
        Task LogoutAsync(Guid userId);
        Task<UserApplication> GetUserAsync(ClaimsPrincipal userPrincipal);
        Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);

    }
}
