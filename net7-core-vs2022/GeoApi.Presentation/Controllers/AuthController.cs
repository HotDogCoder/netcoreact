using System.Threading.Tasks;
using GeoApi.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class RefreshRequest
        {
            public string RefreshToken { get; set; }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var result = await _authService.Register(username, password);
            if (!result)
                return BadRequest("Registration failed.");
            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.Login(request.Username, request.Password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Invalid username or password" });
            return Ok(new { token });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            var newToken = await _authService.RefreshToken(request.RefreshToken);
            if (string.IsNullOrEmpty(newToken))
                return Unauthorized(new { message = "Invalid refresh token" });
            return Ok(new { token = newToken });
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var isValid = await _authService.VerifyToken(token);
            if (!isValid)
                return Unauthorized(new { message = "Invalid token" });
            return Ok(new { isValid });
        }
    }
}
