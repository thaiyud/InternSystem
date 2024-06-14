using InternSystem.Application.Features.CongNgheManagement.Commands;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.Interview
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongNgheController : ApiControllerBase
    {

        [HttpPost("create-cong-nghe")]

        public async Task<IActionResult> CreateCongNghe([FromBody] CreateCongNgheCommand command)
        {

            CreateCongNgheResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPut("update-cong-nghe")]
        public async Task<IActionResult> UpdateCongNghe([FromBody] UpdateCongNgheCommand command)
        {
            UpdateCongNgheResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }


        [HttpDelete("delete-cong-nghe")]
        public async Task<IActionResult> DeleteCongNghe([FromBody] DeleteCongNgheCommand command)
        {
            bool response = await Mediator.Send(command);
            return Ok(response);/*? StatusCode(204) : StatusCode(500, "Delete failed");*/
        }

        [HttpGet("get-all-cong-nghe")]
        public async Task<ActionResult<GetAllCongNgheResponse>> GetAllCongNghe()
        {
            var CongNghe = await Mediator.Send(new GetAllCongNgheQuery());
            return Ok(CongNghe);
        }

        [HttpGet("get-cong-nghe-by-id")]
        public async Task<IActionResult> GetCongNgheById([FromQuery] GetCongNgheByIdQuery query)
        {
            GetCongNgheByIdResponse response = await Mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

    }
}
