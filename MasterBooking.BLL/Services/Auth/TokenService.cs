using MasterBooking.BLL.DTO.Auth;
using MasterBooking.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(
            IOptions<JwtSettings> jwtSettings,
            UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
        }

        public async Task<string> CreateAccessTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),

            new Claim(JwtRegisteredClaimNames.Email, user.Email!),

            new Claim(ClaimTypes.NameIdentifier, user.Id),

            new Claim(ClaimTypes.Name, user.UserName!)
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _jwtSettings.AccessTokenMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
