using InternSystem.API.Utilities;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Commands.User;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Application.Features.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiControllerBase
    {
        [HttpGet("get-all-user")]
        [Authorize(AppConstants.AdminStaffLeader)]
        public async Task<ActionResult<GetAllUserResponse>> GetAllUser() {
            var user = await Mediator.Send(new GetAllUserQuery());
            return Ok(user);
        }

        [HttpPost("create-user-by-admin")]
        //[Authorize(AppConstants.AdminStaffLeader)]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserCommand command)
        {
            try
            {
                return await Mediator.Send(command);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-user-by-admin")]
        [Authorize(AppConstants.AdminStaffLeader)]
        public async Task<ActionResult<CreateUserResponse>> UpdateUser([FromBody] UpdateUserCommand command)
        {
            try
            {
                return await Mediator.Send(command);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("active/inactive-user-by-admin")]
        [Authorize(AppConstants.AdminStaffLeader)]
        public async Task<IActionResult> Deactivate([FromBody] ActiveUserCommand command)
        {
            
            var result = await Mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
        }

    }
}
