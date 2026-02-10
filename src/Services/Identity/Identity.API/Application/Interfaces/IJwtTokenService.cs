using Identity.API.Models;

namespace Identity.API.Application.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateTokenAsync(UserApplication user);
    }
}
