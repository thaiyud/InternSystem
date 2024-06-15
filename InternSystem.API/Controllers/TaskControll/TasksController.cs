using InternSystem.Application.Features.CauHoiCongNgheManagement.Queries;
using InternSystem.Application.Features.CauHoiManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Commands;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Queries;
using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Commands.Delete;
using InternSystem.Application.Features.TaskManage.Commands.Update;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Application.Features.TaskManage.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers.TaskControll
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ApiControllerBase
    {
        [HttpPost("task/create")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
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

        [HttpPut("task/update")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy == null)
            {
                return StatusCode(500, "Cannot get Id from JWT token");
            }
            try
            {
               

                TaskResponse response = await Mediator.Send(command);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("task/delete")]
        public async Task<IActionResult> DeleteTask([FromBody] DeleteTaskCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy == null)
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

        [HttpGet("task/get-by-id")]
        public async Task<IActionResult> GetTaskById([FromQuery] GetTaskByIdQuery query)
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

        [HttpGet("task/get-all")]
        public async Task<IActionResult> GetAllTask([FromQuery]GetTaskQuery query)
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
        [HttpPost("user-task/create")]
        public async Task<IActionResult> AddUserToTask([FromBody] CreateUserTaskCommand command)
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

        [HttpPut("user-task/update")]
        public async Task<IActionResult> UpdateUserTask([FromBody] UpdateUserTaskCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy == null)
            {
                return StatusCode(500, "Cannot get Id from JWT token");
            }
            try
            {
                

                var response = await Mediator.Send(command);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("user-task/delete")]
        public async Task<IActionResult> DeleteUserTask([FromBody] DeleteTaskCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy == null)
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

        [HttpGet("user-task/get-by-id")]
        public async Task<IActionResult> GetUserTaskById([FromQuery]GetUserTaskByIdQuery query)
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

        [HttpGet("user-task/get-all")]
        public async Task<IActionResult> GetAllUserTask([FromQuery]GetUserTaskQuery query)
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
