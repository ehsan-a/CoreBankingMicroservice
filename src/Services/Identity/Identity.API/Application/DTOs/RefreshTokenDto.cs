using Identity.API.Models;

namespace Identity.API.Application.DTOs
{
    public class RefreshTokenDto
    {
        public Guid UserId { get; set; }
        public UserApplication UserApplication { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }
        public string? RevokedReason { get; set; }

        public bool IsExpired => DateTime.Now >= ExpiresAt;
        public bool IsRevoked => RevokedAt != null;
    }
}
