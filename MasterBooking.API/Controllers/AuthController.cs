using MasterBooking.BLL.DTO.Auth;
using MasterBooking.BLL.Services.Auth;
using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MasterBooking.API.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string token)
        {
            var result = await _authService.RefreshAsync(token);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string token)
        {
            await _authService.LogoutAsync(token);
            return Ok();
        }
    }
}
