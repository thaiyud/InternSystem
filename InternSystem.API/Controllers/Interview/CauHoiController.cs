using InternSystem.Application.Features.CauHoiCongNgheManagement.Commands;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Queries;
using InternSystem.Application.Features.CauHoiManagement.Commands;
using InternSystem.Application.Features.CauHoiManagement.Models;
using InternSystem.Application.Features.CauHoiManagement.Queries;
using InternSystem.Application.Features.DuAnManagement.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.Interview
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHoiController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public CauHoiController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("cau-hoi/get-by-id")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetCauHoiById([FromQuery] GetCauHoiByIdQuery query)
        {
            GetCauHoiByIdResponse response = await _mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }
        [HttpGet("cau-hoi/get-all")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllCauHoi([FromQuery] GetAllCauHoiQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("cau-hoi/create")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateCauHoi([FromBody] CreateCauHoiCommand command)
        {
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            //if (command.CreatedBy.IsNullOrEmpty())
            //{
            //    command.CreatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            //}
            // HARD-CODE

            CreateCauHoiResponse response = await _mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPut("cau-hoi/update")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateCauHoi([FromBody] UpdateCauHoiCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            UpdateCauHoiResponse response = await _mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpDelete("cau-hoi/delete")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteCauHoi([FromBody] DeleteCauHoiCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            bool response = await Mediator.Send(command);
            return response ? StatusCode(204) : StatusCode(500, "Delete failed");
        }

        #region Cauhoi cong nghe
        // Cauhoi cong nghe
        [HttpGet("cau-hoi-cong-nghe/get-by-id")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetCauHoiCongNgheById([FromQuery] GetCauHoiCongNgheByIdQuery query)
        {
            GetCauHoiCongNgheByIdResponse response = await _mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }
        
        [HttpGet("cau-hoi-cong-nghe/get-all")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllCauHoiCongNghe([FromQuery] GetAllCauHoiCongNgheQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("cau-hoi-cong-nghe/create")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateCauHoiCongNghe([FromBody] CreateCauHoiCongNgheCommand command)
        {
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            //if (command.CreatedBy.IsNullOrEmpty())
            //{
            //    command.CreatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            //}
            // HARD-CODE

            CreateCauHoiCongNgheResponse response = await _mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPut("cau-hoi-cong-nghe/update")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateCauHoiCongNghe([FromBody] UpdateCauHoiCongNgheCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            UpdateCauHoiCongNgheResponse response = await _mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpDelete("cau-hoi-cong-nghe/delete")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteCauHoiCongNghe([FromBody] DeleteCauHoiCongNgheCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            bool response = await Mediator.Send(command);
            return response ? StatusCode(204) : StatusCode(500, "Delete failed");
        }
        #endregion
    }
}
