using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Commands.Delete;
using InternSystem.Application.Features.TaskManage.Commands.Update;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Application.Features.TaskManage.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers.TaskControll
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportTasksController : ApiControllerBase
    {
        [HttpPost("report-task/create")]
        public async Task<IActionResult> Create([FromBody] CreateTaskReportCommand command)
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

        [HttpPut("report-task/update")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskReportCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy == null)
            {
                return StatusCode(500, "Cannot get Id from JWT token");
            }
            try
            {
                

                TaskReportResponse response = await Mediator.Send(command);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("report-task/delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteTaskReportCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy == null)
            {
                return StatusCode(500, "Cannot get Id from JWT token");
            }
            try
            {
               
                var result = await Mediator.Send(command);
                return StatusCode(204, "Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Delete failed");
            }
        }

        [HttpGet("report-task/get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] GetTaskReportByIdQuery query)
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

        [HttpGet("report-task/get-all")]
        public async Task<IActionResult> GetAll([FromQuery] GetTaskReportQuery query)
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
