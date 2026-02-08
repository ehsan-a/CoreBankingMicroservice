using System.Security.Claims;

namespace Shared.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (!Guid.TryParse(principal.FindFirst("sub")?.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user id claim");
            }
            return userId;
        }

        public static string? GetUserName(this ClaimsPrincipal principal) =>
            principal.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
    }
}
