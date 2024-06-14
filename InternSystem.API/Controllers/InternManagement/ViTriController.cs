using InternSystem.Application.Features.ViTriManagement.Commands;
using InternSystem.Application.Features.ViTriManagement.Models;
using InternSystem.Application.Features.ViTriManagement.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.InternManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitriController : ApiControllerBase
    {
            [HttpGet("get-by-id")]
            [Authorize(Roles = "Staff")]
            public async Task<IActionResult> GetViTriById([FromQuery] GetViTriByIdQuery query)
            {
                GetViTriByIdResponse response = await Mediator.Send(query);
                if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

                return Ok(response);
            }

            [HttpPost("create")]
            [Authorize(Roles = "Staff")]
            public async Task<IActionResult> CreateViTri([FromBody] CreateViTriCommand command)
            {
                command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

                CreateViTriResponse response = await Mediator.Send(command);
                if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

                return Ok(response);
            }

            [HttpPut("update")]
            [Authorize(Roles = "Staff")]
            public async Task<IActionResult> UpdateViTri([FromBody] UpdateViTriCommand command)
            {
                command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

                UpdateViTriResponse response = await Mediator.Send(command);
                if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

                return Ok(response);
            }

            [HttpDelete("delete")]
            [Authorize(Roles = "Staff")]
            public async Task<IActionResult> DeleteViTri([FromBody] DeleteViTriCommand command)
            {
                command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

                bool response = await Mediator.Send(command);
                return response ? StatusCode(204) : StatusCode(500, "Delete failed");
            }
        }
}
