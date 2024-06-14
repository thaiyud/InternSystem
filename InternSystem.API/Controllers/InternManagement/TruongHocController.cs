using InternSystem.Application.Features.TruongHocManagement.Commands;
using InternSystem.Application.Features.TruongHocManagement.Models;
using InternSystem.Application.Features.TruongHocManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.InternManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruongHocController : ApiControllerBase
    {
        [HttpGet("get-by-id")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetTruongHocById([FromQuery] GetTruongHocByIdQuery query)
        {
            GetTruongHocByIdResponse response = await Mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);

        }

        [HttpPost("create")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateTruongHoc([FromBody] CreateTruongHocCommand command)
        {
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            if (command.CreatedBy.IsNullOrEmpty())
            {
                command.CreatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            CreateTruongHocResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return CreatedAtAction(nameof(GetTruongHocById), new { id = response.Id }, response);
        }

        [HttpPut("update")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateTruongHoc([FromBody] UpdateTruongHocCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            if (command.LastUpdatedBy.IsNullOrEmpty())
            {
                command.LastUpdatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            UpdateTruongHocResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }


        [HttpDelete("delete")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteTruongHoc([FromBody] DeleteTruongHocCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            if (command.DeletedBy.IsNullOrEmpty())
            {
                command.DeletedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            bool response = await Mediator.Send(command);
            if (!response) return StatusCode(500, "DeleteThongBao failed");

            return NoContent();
        }
    }
}
