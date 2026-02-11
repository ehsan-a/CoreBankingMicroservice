using System.Security.Claims;

namespace Shared.ServiceDefaults
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("Invalid user id claim");

            return userId;
        }

        public static string? GetUserName(this ClaimsPrincipal principal) =>
                principal.FindFirst(ClaimTypes.Name)?.Value;

    }
}
