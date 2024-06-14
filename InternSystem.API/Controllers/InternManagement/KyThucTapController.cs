using InternSystem.Application.Features.KyThucTapManagement.Commands;
using InternSystem.Application.Features.KyThucTapManagement.Models;
using InternSystem.Application.Features.KyThucTapManagement.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.InternManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class KyThucTapController : ApiControllerBase
    {
        [HttpGet("get-by-id")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetKyThucTapById([FromQuery] GetKyThucTapByIdQuery query)
        {
            GetKyThucTapByIdResponse response = await Mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPost("create")]
        // [Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateKiThucTap([FromBody] CreateKyThucTapCommand command)
        {
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            if (command.CreatedBy.IsNullOrEmpty())
            {
                command.CreatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            CreateKyThucTapResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return CreatedAtAction(nameof(GetKyThucTapById), new { id = response.Id }, response);
        }

        [HttpPut("update")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateKyThucTap([FromBody] UpdateKyThucTapCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            if (command.LastUpdatedBy.IsNullOrEmpty())
            {
                command.LastUpdatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            UpdateKyThucTapResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }


        [HttpDelete("delete")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteKyThucTap([FromBody] DeleteKyThucTapCommand command)
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
            return response ? StatusCode(204) : StatusCode(500, "DeleteThongBao failed");
        }
    }
}
