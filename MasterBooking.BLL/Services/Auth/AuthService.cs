using MasterBooking.BLL.DTO.Auth;
using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            AppDbContext context,
            IConfiguration config)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
            _jwtSettings = config.GetSection("Jwt").Get<JwtSettings>()!;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new Exception("Email already exists");

            var user = new ApplicationUser
            {
                Name = dto.Name,
                Email = dto.Email,
                UserName = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception("User creation failed");

            var allowedRoles = new[] { "Client", "Master" };

            var role = allowedRoles.Contains(dto.Role)
                ? dto.Role
                : "Client";

            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new Exception("Invalid credentials");

            var valid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!valid)
                throw new Exception("Invalid credentials");

            var accessToken = await _tokenService.CreateAccessTokenAsync(user);

            var refreshToken = _tokenService.GenerateRefreshToken();

            var entity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays)
            };

            _context.RefreshTokens.Add(entity);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDto> RefreshAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            var newAccessToken =
                await _tokenService.CreateAccessTokenAsync(token.User);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (token == null)
                return;

            token.IsRevoked = true;

            await _context.SaveChangesAsync();
        }
    }
}
