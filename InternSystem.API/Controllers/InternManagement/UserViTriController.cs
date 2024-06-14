using InternSystem.Application.Features.UserViTriManagement.Commands;
using InternSystem.Application.Features.UserViTriManagement.Models;
using InternSystem.Application.Features.UserViTriManagement.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.InternManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserViTriController : ApiControllerBase
    {
            [HttpGet("get-by-id")]
            [Authorize(Roles = "Staff")]
            public async Task<IActionResult> GetUserViTriById([FromQuery] GetUserViTriByIdQuery query)
            {
                GetUserViTriByIdResponse response = await Mediator.Send(query);
                if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

                return Ok(response);
            }

            [HttpPost("create")]
            [Authorize(Roles = "Staff")]
            public async Task<IActionResult> CreateUserViTri([FromBody] CreateUserViTriCommand command)
            {
                command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");


                CreateUserViTriResponse response = await Mediator.Send(command);
                if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

                return Ok(response);
            }

            [HttpPut("update")]
            [Authorize(Roles = "Staff")]
            public async Task<IActionResult> UpdateUserViTri([FromBody] UpdateUserViTriCommand command)
            {
                command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

                UpdateUserViTriResponse response = await Mediator.Send(command);
                if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

                return Ok(response);
            }

            [HttpDelete("delete")]
            [Authorize(Roles = "Staff")]
            public async Task<IActionResult> DeleteUserViTri([FromBody] DeleteUserViTriCommand command)
            {
                command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

                bool response = await Mediator.Send(command);
                return response ? StatusCode(204) : StatusCode(500, "Delete failed");
            }
        }
}
