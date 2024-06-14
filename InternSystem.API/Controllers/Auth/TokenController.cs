using InternSystem.Application.Features.Token.Models;
using InternSystem.Application.Common.Behaviors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Cryptography;
using InternSystem.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace InternSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ApiControllerBase
    {
        public static Dictionary<string, GenerateTokenCommand> UserStore = new Dictionary<string, GenerateTokenCommand>();

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenRequest request)
        {
            var tokenResponse = await Mediator.Send(new GenerateTokenCommand
            {
                UserId = request.UserId,
                Role = request.Role
            });

            return Ok(tokenResponse);
        }
    }
}
