using InternSystem.Application.Features.ThongBaoManagement.Commands;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using InternSystem.Application.Features.ThongBaoManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.Communication
{
    [Route("api/thongbao")]
    [ApiController]
    public class ThongBaoController : ApiControllerBase
    {

        [HttpPost("create")]
        //[Authorize]
        public async Task<IActionResult> CreateThongBao([FromBody] CreateThongBaoCommand command)
        {
            command.CreateBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.CreateBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE, for testing purposes
            if (command.CreateBy.IsNullOrEmpty())
            {
                command.CreateBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            CreateThongBaoResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Created(nameof(GetThongBaoById), response);
        }

        [HttpPut("update")]
        //[Authorize]
        public async Task<IActionResult> UpdateThongBao([FromBody] UpdateThongBaoCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE, for testing purposes
            if (command.LastUpdatedBy.IsNullOrEmpty())
            {
                command.LastUpdatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            UpdateThongBaoResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);

        }

        [HttpDelete("delete")]
        //[Authorize]
        public async Task<IActionResult> DeleteThongBao([FromBody] DeleteThongBaoCommand command)
        {
            DeleteThongBaoResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return NoContent();

        }

        [HttpGet("by-id")]
        //[Authorize]
        public async Task<IActionResult> GetThongBaoById([FromQuery] int id)
        {
            GetThongBaoByIdResponse response = await Mediator.Send(new GetThongBaoByIdQuery()
            {
                Id = id
            });
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllThongBao([FromQuery] int pageNum, int pageSize)
        {
            GetAllThongBaoResponse response = await Mediator.Send(new GetAllThongBaoQuery()
            {
                PageNumber = pageNum,
                PageSize = pageSize
            });

            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response.ThongBaos);
        }

    }
}
