using CsvHelper;
using System.Globalization;
using InternSystem.Application.Features.InternManagement.Commands;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using InternSystem.Application.Features.Interview.Commands;
using InternSystem.Application.Features.Interview.Models;
using InternSystem.Application.Features.Interview.Queries;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InternSystem.API.Controllers.InternManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternInfoController : ApiControllerBase
    {
        [HttpGet("get-by-id")]
        //[Authorize (Roles = "Staff")]
        public async Task<IActionResult> GetInternInfoById([FromQuery] GetInternInfoByIdQuery query)
        {

            GetInternInfoByIdResponse response = await Mediator.Send(query);
            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            return Ok(response);
        }

        [HttpPost("create")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateInternInfo([FromBody] CreateInternInfoCommand command)
        {
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            if (command.CreatedBy.IsNullOrEmpty())
            {
                command.CreatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            CreateInternInfoResponse response = await Mediator.Send(command);

            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);

            //return Ok(response);
            return CreatedAtAction(nameof(GetInternInfoById), new { id = response.Id }, response);
        }

        [HttpPut("self-update")]
        //[Authorize(Roles = "Intern")]
        public async Task<IActionResult> SelfUpdateInternInfo([FromBody] SelfUpdateInternInfoCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            if (command.LastUpdatedBy.IsNullOrEmpty())
            {
                command.LastUpdatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            UpdateInternInfoResponse response = await Mediator.Send(command);

            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);
            return Ok(response);
        }

        [HttpPut("update")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateInternInfo([FromBody] UpdateInternInfoCommand command)
        {
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            if (command.LastUpdatedBy.IsNullOrEmpty())
            {
                command.LastUpdatedBy = "49c087c3-5913-4938-82fd-7c5e8fdfb83f";
            }
            // HARD-CODE

            UpdateInternInfoResponse response = await Mediator.Send(command);

            if (!response.Errors.IsNullOrEmpty()) return StatusCode(500, response.Errors);
            return Ok(response);
        }

        [HttpDelete("delete")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteInternInfo([FromQuery] DeleteInternInfoCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            // HARD-CODE
            // HARD-CODE

            bool response = await Mediator.Send(command);
            return response ? StatusCode(204) : StatusCode(500, "DeleteThongBao failed");
        }

        [HttpGet("csv/download-template")]
        public IActionResult DownloadCsvTemplate()
        {
            var template = new List<CsvTemplate>
        {
            new CsvTemplate()
        };

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(template);
                streamWriter.Flush();
                memoryStream.Position = 0;
                return File(memoryStream.ToArray(), "text/csv", "UserTemplate.csv");
            }
        }

        [HttpPost("csv/upload")]
        public async Task<IActionResult> UploadCsv([FromForm] ImportCsvCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return Ok("File processed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing file: {ex.Message}");
            }
        }

        [HttpGet("get-all-user-email-status")]
        //[Authorize(Roles = "Staff,Leader")]
        public async Task<ActionResult<GetDetailEmailUserStatusResponse>> GetAllUserEmailStatus()
        {
            var status = await Mediator.Send(new GetAllEmailUserStatusQuery());
            if (!status.Any())
            {
                return StatusCode(500, "No email user status exists");
            }
            return Ok(status);
        }

        [HttpGet("get-user-email-status-by-id")]
        //[Authorize(Roles = "Staff,Leader")]
        public async Task<IActionResult> GetEmailUserStatusById([FromQuery] GetEmailUserStatusByIdQuery query)
        {
            var result = await Mediator.Send(query);
            if (!result.Errors.IsNullOrEmpty()) return StatusCode(500, result.Errors);
            return Ok(result);
        }

        [HttpPost("create-email-user-status")]
        //[Authorize(Roles = "Staff,Leader")]
        public async Task<ActionResult<GetDetailEmailUserStatusResponse>> CreateEmailUserStatus([FromBody] CreateEmailUserStatusCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Errors.IsNullOrEmpty()) return StatusCode(500, result.Errors);
            return Ok(result);
        }

        [HttpPut("update-email-user-status")]
        //[Authorize(Roles = "Staff,Leader")]
        public async Task<ActionResult<GetDetailEmailUserStatusResponse>> UpdateEmailUserStatus([FromBody] UpdateEmailUserStatusCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Errors.IsNullOrEmpty()) return StatusCode(500, result.Errors);
            return Ok(result);
        }

        [HttpDelete("delete-email-user-status")]
        //[Authorize(Roles = "Staff,Leader")]
        public async Task<IActionResult> DeleteEmailUserStatus([FromBody] DeleteEmailUserStatusCommand command)
        {
            bool result = await Mediator.Send(command);
            return result ? StatusCode(200, "Delete successfully") : StatusCode(500, "Delete failed");
        }
    }
}
