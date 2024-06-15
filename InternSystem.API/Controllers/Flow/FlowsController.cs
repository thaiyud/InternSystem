using InternSystem.Application.Features.CongNgheManagement.Commands;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.TaskManage.Commands;
using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Commands.Update;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Application.Features.TaskManage.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InternSystem.API.Controllers.Flow
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowsController : ApiControllerBase
    {
        [HttpPost("userNhomZalo/create-by-id-phongvan")]

        public async Task<IActionResult> CreateUserNhomZaloByIdPhongvan([FromBody] CreateUserToNhomZaloByIdCommand command)
        {
            command.CreateBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.CreateBy == null)
            {
                return StatusCode(500, "Cannot get Id from JWT token");
            }
            try
            {
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("nhomZaloTask/create")]
        public async Task<IActionResult> CreateNhomZaloTask([FromBody] CreateNhomZaloTaskCommand command)
        {
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.CreatedBy == null)
            {
                return StatusCode(500, "Cannot get Id from JWT token");
            }
            try
            {
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("promote-intern-to-leader")]
        public async Task<IActionResult> InternToLeader([FromBody] PromoteMemberToLeaderCommand command)
        {
           
            if (User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value==null)
            {
                return StatusCode(500, "Cannot get Id from JWT token");
            }
            try
            {
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("nhomZaloTask/update")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateNhomZaloTaskCommand command)
        {
            try
            {
                command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (command.LastUpdatedBy == null)
                {
                    return StatusCode(500, "Cannot get Id from JWT token");
                }

                var response = await Mediator.Send(command);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("nhomZaloTask/get-all")]
        public async Task<IActionResult> GetAllTask([FromQuery] GetNhomZaloTaskByQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
