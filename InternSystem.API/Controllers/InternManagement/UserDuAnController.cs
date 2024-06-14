using InternSystem.Application.Features.UserDuAnManagement.Commands;
using InternSystem.Application.Features.UserDuAnManagement.Models;
using InternSystem.Application.Features.UserDuAnManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.InternManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDuAnController : ApiControllerBase
    {
        [HttpGet("get-by-id")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetUserDuAnById([FromQuery] GetUserDuAnByIdQuery query)
        {
            GetUserDuAnByIdResponse response = await Mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateUserDuAn([FromBody] CreateUserDuAnCommand command)
        {
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            CreateUserDuAnResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateUserDuAn([FromBody] UpdateUserDuAnCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            UpdateUserDuAnResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteUserDuAn([FromBody] DeleteUserDuAnCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            bool response = await Mediator.Send(command);
            return response ? StatusCode(204) : StatusCode(500, "Delete failed");
        }
    }
}