using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Models.ResetTokenModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class ResetTokenCommandHandler : IRequestHandler<ResetTokenCommand, ResetTokenResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public ResetTokenCommandHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResetTokenResponse> Handle(ResetTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByRefreshTokenAsync(request.ResetTokenRequest.ResetToken);
            if (user == null || user.ResetTokenExpires <= DateTimeOffset.UtcNow)
            {
                throw new SecurityTokenException("Invalid or expired refresh token");
            }
            var newVerificationToken = GenerateVerificationToken(user.Id);
            var newResetToken = GenerateResetToken();

            user.ResetToken = newVerificationToken;
            user.ResetTokenExpires = DateTimeOffset.UtcNow.AddHours(1);
            await _unitOfWork.UserRepository.UpdateUserAsync(user);

            return new ResetTokenResponse
            {
                VerificationToken = newVerificationToken,
                ResetToken = newResetToken
            };
        }

        private string GenerateVerificationToken(string userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, userId) },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateResetToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
