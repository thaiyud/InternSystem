using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Models.LoginModels;
using InternSystem.Application.Features.User.Models;
using InternSystem.Application.Features.User.Queries;
using Microsoft.AspNetCore.Mvc;
using static InternSystem.Application.Features.User.Commands.AddRoleCommandValidation;
using Microsoft.AspNetCore.Identity;
using InternSystem.Domain.Entities;
using InternSystem.Application.Features.Token.Models;
using Microsoft.AspNetCore.Authorization;

namespace InternSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetSecureData()
        {
            return Ok("This is a secure data.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await Mediator.Send(new LoginCommand(request));
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenRequest request)
        {
            try
            {
                var response = await Mediator.Send(new RefreshTokenCommand(request));
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // Password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await Mediator.Send(new ForgotPasswordCommand(request.Email));
            return Ok();
        }

        [HttpPost("check-valid-code")]
        public async Task<IActionResult> CheckValidCode([FromBody] CheckValidCode request)
        {
            var isValid = await Mediator.Send(new VerifyCodeCommand(request.Email, request.Code));
            if (!isValid)
            {
                return BadRequest("Invalid code.");
            }
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await Mediator.Send(new ResetPasswordCommand(request.Email, request.NewPassword, request.ConfirmPassword));
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }
    }
}
