using InternSystem.Application.Features.CongNgheDuAnManagement.Commands;
using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using InternSystem.Application.Features.CongNgheDuAnManagement.Queries;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.Interview
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongNgheDuAnController : ApiControllerBase
    {

        [HttpPost("create-cong-nghe-du-an")]

        public async Task<IActionResult> CreateCongNgheDuAn([FromBody] CreateCongNgheDuAnCommand command)
        {

            CreateCongNgheDuAnResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPut("update-cong-nghe-du-an")]
        public async Task<IActionResult> UpdateCongNgheDuAn([FromBody] UpdateCongNgheDuAnCommand command)
        {
            UpdateCongNgheDuAnResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }


        [HttpDelete("delete-cong-nghe-du-an")]
        public async Task<IActionResult> DeleteCongNgheDuAn([FromBody] DeleteCongNgheDuAnCommand command)
        {
            bool response = await Mediator.Send(command);
            return Ok(response);/*? StatusCode(204) : StatusCode(500, "Delete failed");*/
        }

        [HttpGet("get-all-cong-nghe-du-an")]
        public async Task<ActionResult<GetAllCongNgheDuAnResponse>> GetAllCongNgheDuAn()
        {
            var CongNgheDuAn = await Mediator.Send(new GetAllCongNgheDuAnQuery());
            return Ok(CongNgheDuAn);
        }

        [HttpGet("get-cong-nghe-du-an-by-id")]
        public async Task<IActionResult> GetCongNgheDuAnById([FromQuery] GetCongNgheDuAnByIdQuery query)
        {
            GetCongNgheDuAnByIdResponse response = await Mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }
    }
}
