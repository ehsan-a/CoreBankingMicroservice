using Identity.API.Application.DTOs;
using Identity.API.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentitiesController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentitiesController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        [Authorize(Policy = "Accessibility")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _identityService.RegisterAsync(request);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authenticates a user")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequest request)
        {
            var result = await _identityService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var user = await _identityService.GetUserAsync(User);
            await _identityService.LogoutAsync(user.Id);
            return Ok();
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var result = await _identityService.RefreshTokenAsync(refreshToken);
            return Ok(new { accessToken = result.AccessToken, refreshToken = result.RefreshToken });
        }
    }
}
