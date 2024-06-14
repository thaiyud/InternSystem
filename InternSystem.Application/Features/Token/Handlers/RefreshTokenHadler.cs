
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Token.Models;
using InternSystem.Application.Features.User.Models.ResetTokenModels;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Token.Handlers
{
    public class RefreshTokenHadler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUser> _userManager;

        public RefreshTokenHadler(IConfiguration configuration, IUnitOfWork unitOfWork, UserManager<AspNetUser> userManager)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByRefreshTokenAsync(request.RefreshToken.RefreshToken);
            if (user == null || user.ResetTokenExpires <= DateTimeOffset.UtcNow)
            {
                throw new SecurityTokenException("Invalid or expired refresh token");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();
            if(userRole == null)
            {
                throw new SecurityTokenException("User dont have permission");
            }
            var newAccessToken = GenerateAccessToken(user.Id, userRole);
            var _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);

            // save database
            user.VerificationToken = newAccessToken;
            user.VerificationTokenExpires = DateTimeOffset.UtcNow.AddMinutes(_exAccessToken);
            await _unitOfWork.UserRepository.UpdateUserAsync(user);
            //
            return new TokenResponse
            {
               AccessToken = newAccessToken,
               RefreshToken = user.ResetToken,
            };
        }
        private string GenerateAccessToken(string userId, string role)
        {
            var _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);

            var keyString = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Id", userId),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("isRefreshToken", "false")
        };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(_exAccessToken),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
