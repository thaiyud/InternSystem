using InternSystem.Application.Features.Token.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace YourNamespace.Features.Token.Handlers
{
    // Generate JWT  - backup by 'tqthai'
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, TokenResponse>
    {
        private readonly IMediator _mediator;
        private const string JwtKey = "6f1f3d28a486ec27d52cd5552954a81940d2a4c1d98";

        public GenerateTokenCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TokenResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            // Generate access token
            var accessToken = GenerateToken(request.UserId, request.Role, false);

            // Generate refresh token
            var refreshToken = GenerateToken(request.UserId, request.Role, true);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateToken(string userId, string role, bool isRefreshToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userId),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (isRefreshToken)
            {
                claims.Add(new Claim("isRefreshToken", "true"));
            }

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5284",
                audience: "InternSystem",
                claims: claims,
                expires: isRefreshToken ? DateTime.Now.AddHours(24) : DateTime.Now.AddMinutes(60), // Token expiry time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

     
    }
}
