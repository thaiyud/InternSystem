using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Token.Models;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Models.LoginModels;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InternSystem.Application.Features.User.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly int _exAccessToken;
        private readonly int _exRefreshToken;

        public LoginCommandHandler(IConfiguration configuration, UserManager<AspNetUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);
            _exRefreshToken = int.Parse(_configuration["Jwt:ExpirationRefreshToken"]!);
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.LoginRequest.Username);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.LoginRequest.Password);
            if (!checkPassword)
            {
                throw new ArgumentException("Password is not correct");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();
            if (userRole == null)
            {
                throw new SecurityTokenException("User dont have permission");
            }

            // Generate access token
            var accessToken = GenerateToken(user.Id, userRole, false);

            // Generate refresh token
            var refreshToken = GenerateToken(user.Id, userRole, true);

            // Save Database

            user.VerificationToken = accessToken;
            user.ResetToken = refreshToken;
            user.VerificationTokenExpires = DateTimeOffset.Now.AddHours(_exRefreshToken);
            user.ResetTokenExpires = DateTimeOffset.Now.AddMinutes(_exAccessToken);
            await _userManager.UpdateAsync(user);
            return new LoginResponse
            {
                VerificationToken = accessToken,
                ResetToken = refreshToken
            };
        }

        private string GenerateToken(string userId, string role, bool isRefreshToken)
        {
            var keyString = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
            {
                new Claim("Id", userId),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (isRefreshToken)
            {
                claims.Add(new Claim("isRefreshToken", "true"));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: isRefreshToken ? DateTime.Now.AddHours(_exRefreshToken) : DateTime.Now.AddMinutes(_exAccessToken), // Token expiry time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
