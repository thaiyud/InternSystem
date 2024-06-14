using CsvHelper.Configuration.Attributes;
using InternSystem.API.Utilities;
using InternSystem.Application.Features.DashboardManage;
using InternSystem.Application.Features.DashboardManage.Models;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Application.Features.DuAnManagement.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.InternManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuAnController : ApiControllerBase
    {
        [HttpGet("get-by-id")]
        [Authorize(Roles = AppConstants.AdminStaffLeader)]
        public async Task<IActionResult> GetDuAnById([FromQuery] GetDuAnByIdQuery query)
        {
            GetDuAnByIdResponse response = await Mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPost("create")]
        [Authorize(Roles = AppConstants.AdminStaffLeader)]
        public async Task<IActionResult> CreateDuAn([FromBody] CreateDuAnCommand command)
        {
                command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            CreateDuAnResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

                return Ok(response);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateDuAn([FromBody] UpdateDuAnCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            UpdateDuAnResponse response = await Mediator.Send(command);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteDuAn([FromBody] DeleteDuAnCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            bool response = await Mediator.Send(command);
            return response ? StatusCode(204) : StatusCode(500, "DeleteThongBao failed");
        }
    }
}
