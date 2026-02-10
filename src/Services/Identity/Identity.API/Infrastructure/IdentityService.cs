using FluentValidation;
using Identity.API.Application.DTOs;
using Identity.API.Application.Interfaces;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Identity.API.Infrastructure
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly ILogger<IdentityService> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IValidator<LoginRequest> _validator;
        private readonly IValidator<RegisterRequest> _registerValidator;

        public IdentityService(
            UserManager<UserApplication> userManager,
            ILogger<IdentityService> logger,
            IJwtTokenService jwtTokenService,
            IRefreshTokenService refreshTokenService,
            IValidator<LoginRequest> validator,
            IValidator<RegisterRequest> registerValidator)
        {
            _userManager = userManager;
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
            _validator = validator;
            _registerValidator = registerValidator;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequest input)
        {
            await _validator.ValidateAndThrowAsync(input);
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            var validPassword = await _userManager.CheckPasswordAsync(user, input.Password);
            if (!validPassword)
                throw new UnauthorizedAccessException("Invalid email or password");

            _logger.LogInformation("User logged in successfully.");

            var accessToken = await _jwtTokenService.GenerateTokenAsync(user);
            var refreshToken = await _refreshTokenService.GenerateTokenAsync(user.Id);
            return new LoginResponseDto { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task RegisterAsync(RegisterRequest input)
        {
            await _registerValidator.ValidateAndThrowAsync(input);
            var user = new UserApplication(input.CustomerId);
            user.UserName = input.Email;
            user.Email = input.Email;

            var result = await _userManager.CreateAsync(user, input.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }

            _logger.LogInformation("User registered successfully.");
        }

        public async Task LogoutAsync(Guid userId)
        {
            await _refreshTokenService.RevokeTokenAsync(userId, "Logout");
            await _refreshTokenService.SaveChangesAsync();
            _logger.LogInformation("User logged out successfully.");
        }

        public async Task<UserApplication> GetUserAsync(ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal == null)
                throw new ArgumentNullException(nameof(userPrincipal));

            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
                throw new BadHttpRequestException("Invalid Request");

            return user;
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var token = await _refreshTokenService.GetTokenAsync(refreshToken);

            if (token == null || token.IsExpired || token.IsRevoked)
                throw new UnauthorizedAccessException("Invalid refresh token");

            token.RevokedAt = DateTime.Now;
            token.RevokedReason = "Rotated";
            await _refreshTokenService.SaveChangesAsync();

            var newRefreshToken = await _refreshTokenService.GenerateTokenAsync(token.UserId);
            var newAccessToken = await _jwtTokenService.GenerateTokenAsync(token.UserApplication);

            return (newAccessToken, newRefreshToken);
        }
    }
}
