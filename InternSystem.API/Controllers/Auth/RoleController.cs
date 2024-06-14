using InternSystem.API.Utilities;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Models;
using InternSystem.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static InternSystem.Application.Features.User.Commands.AddRoleCommandValidation;
using static InternSystem.Application.Features.User.Commands.Role.DeleteRoleCommandValidation;
using static InternSystem.Application.Features.User.Commands.UpdateRoleCommandValidation;

namespace InternSystem.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ApiControllerBase
    {
        [HttpGet("get-all-role")]
        [Authorize(AppConstants.AdminRole)]
        public async Task<ActionResult<GetRoleResponse>> GetAllRole()
        {
            var user = await Mediator.Send(new GetRoleQuery());

            return Ok(user);
        }
        //================================================================================
        [HttpPost("create-role")]
        //[Authorize(AppConstants.AdminRole)]
        public async Task<IActionResult> CreateRole([FromBody] AddRoleCommand command)
        {
            var result = await Mediator.Send(command);
            if (result)
            {
                return Ok("Role created successfully.");
            }
            return BadRequest("Role already exists or could not be created.");
        }
        //================================================================================
        [HttpPut("update-role")]
        [Authorize(AppConstants.AdminRole)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
        {
            var result = await Mediator.Send(command);
            if (result)
            {
                return Ok("Role updated successfully.");
            }
            return BadRequest("Role is not match or could not be updated.");
        }
        //================================================================================
        [HttpPost("delete-role")]
        [Authorize(AppConstants.AdminRole)]
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleCommand command)
        {
            var result = await Mediator.Send(command);
            if (result)
            {
                return Ok("Role deleted successfully.");
            }
            return BadRequest("Role is not match or could not be deleted.");
        }
        //================================================================================
        [HttpPost("add-user-to-role")]
        [Authorize(AppConstants.AdminRole)]
        public async Task<IActionResult> AddUserToRole([FromBody] AddUserToRoleCommand command)
        {
            var result = await Mediator.Send(command);
            if (result)
            {
                return Ok("User added to role successfully");
            }
            return BadRequest("Failed to add user to role");
        }
        //================================================================================

    }
}
