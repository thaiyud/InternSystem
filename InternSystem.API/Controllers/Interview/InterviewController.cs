using InternSystem.Application.Features.Auth.Models;
using InternSystem.Application.Features.Auth.Queries;
using InternSystem.Application.Features.Interview.Commands;
using InternSystem.Application.Features.Interview.Models;
using InternSystem.Application.Features.Interview.Queries;
using InternSystem.Application.Features.LichPhongVanManagement.Commands;
using InternSystem.Application.Features.LichPhongVanManagement.Queries;
using InternSystem.Application.Features.PhongVanManagement.Commands;
using InternSystem.Application.Features.PhongVanManagement.Queries;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : ApiControllerBase
    {
        private static List<string> selectedEmails = new List<string>();

        [HttpGet("show-emails-with-indices")]
        public async Task<ActionResult<IEnumerable<EmailWithIndexResponse>>> ShowEmailsWithIndices()
        {
            var emailsWithIndices = await Mediator.Send(new GetEmailsWithIndicesQuery());
            return Ok(emailsWithIndices);
        }

        [HttpPost("select-emails")]
        public async Task<ActionResult> SelectEmails([FromBody] EmailSelectionRequest request)
        {
            var selectedEmailsResult = await Mediator.Send(new SelectEmailsCommand(request.Indices));

            if (!selectedEmailsResult.Any())
            {
                return BadRequest("No emails selected. Please select at least one email.");
            }

            selectedEmails = selectedEmailsResult.ToList();

            return Ok(selectedEmailsResult);
        }

        [HttpGet("show-email-types")]
        public ActionResult<IEnumerable<string>> GetEmailTypes()
        {
            var emailTypes = new List<string> { "Interview Date", "Interview Result", "Internship Time", "Internship Information" };
            return Ok(emailTypes);
        }

        [HttpPost("send-emails")]
        public async Task<ActionResult> SendEmails([FromBody] SendEmailsRequest request)
        {
            if (!selectedEmails.Any())
            {
                return BadRequest("No emails selected. Please select at least one email.");
            }

            var result = await Mediator.Send(new SendEmailsCommand(selectedEmails, request.Subject, request.Body, request.EmailType));

            if (result)
            {
                return Ok("Email sent successfully.");
            }
            else
            {
                return StatusCode(500, "Error sending email.");
            }
        }
        [HttpPost("create-lichphongvan")]
        public async Task<ActionResult> CreateLichPhongVan(CreateLichPhongVanCommand command)
        {
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");
            var response = await Mediator.Send(command);

            if (response.Id != 0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }
        [HttpGet("view-lichphongvan-today")]
        public async Task<ActionResult<IEnumerable<LichPhongVan>>> GetLichPhongVanByToday()
        {
            var query = new GetLichPhongVanByTodayQuery();
            var lichPhongVanList = await Mediator.Send(query);

            return Ok(lichPhongVanList);
        }
        [HttpGet("view-all-lichphongvan")]
        public async Task<ActionResult<LichPhongVan>> GetLichPhongVanById()
        {
            var lichPhongVan = await Mediator.Send(new GetAllLichPhongVanQuery());
            return Ok(lichPhongVan);
        }
        [HttpGet("view-lichphongvan-by-id")]
        public async Task<ActionResult<LichPhongVan>> GetLichPhongVanById([FromQuery] GetLichPhongVanByIdQuery query)
        {
            var lichPhongVan = await Mediator.Send(query);

            if (lichPhongVan == null || lichPhongVan.Errors != null)
            {
                return NotFound(lichPhongVan?.Errors ?? "Lich phong van not found");
            }

            return Ok(lichPhongVan);
        }
        [HttpDelete("delete-lichphongvan-by-id")]
        public async Task<IActionResult> DeleteLichPhongVan([FromBody] DeleteLichPhongVanCommand command)
        {
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");
            var result = await Mediator.Send(command);

            if (!result)
            {
                return NotFound(new { Message = "Lich phong van not found or already deleted" });
            }

            return Ok(new { Message = "Lich phong van deleted successfully" });
        }
        [HttpPut("update-lichphongvan-by-id")]
        public async Task<IActionResult> UpdateLichPhongVan([FromBody] UpdateLichPhongVanCommand command)
        {
            var result = await Mediator.Send(command);
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            if (result.Errors != null)
            {
                return BadRequest(new { Message = result.Errors });
            }

            return Ok(new { Message = "Lich phong van updated successfully", Data = result });
        }
        [HttpGet("get-inter-on-interview-day")]
        public async Task<IActionResult> GetInfoInterOfInterviewInDay([FromQuery] DateTime? day)
        {
            if (day == null)
            {
                return BadRequest("The date must be provided.");
            }

            var command = new GetFilteredInternInfoByDayQuery { Day = day };
            var result = await Mediator.Send(command);

            if (result == null || !result.Any())
            {
                return NotFound("No interviews found for the provided date.");
            }

            return Ok(result);
        }
        [HttpPost("create-phongvan")]
        public async Task<ActionResult> CreatePhongVan(CreatePhongVanCommand command)
        {
            var response = await Mediator.Send(command);
            command.CreatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.CreatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");

            if (response.Id != 0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }
        [HttpGet("view-all-phongvan")]
        public async Task<ActionResult<PhongVan>> GetPhongVanById()
        {
            var PhongVan = await Mediator.Send(new GetAllPhongVanQuery());
            return Ok(PhongVan);
        }
        [HttpGet("view-phongvan-by-id")]
        public async Task<ActionResult<PhongVan>> GetPhongVanById([FromQuery] GetPhongVanByIdQuery query)
        {
            var phongVan = await Mediator.Send(query);

            if (phongVan == null || phongVan.Errors != null)
            {
                return NotFound(phongVan?.Errors ?? "Phong van not found");
            }

            return Ok(phongVan);
        }
        [HttpPut("update-phongvan-by-id")]
        public async Task<IActionResult> UpdatePhongVan([FromBody] UpdatePhongVanCommand command)
        {
            var result = await Mediator.Send(command);
            command.LastUpdatedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.LastUpdatedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");


            if (result.Errors != null)
            {
                return BadRequest(new { Message = result.Errors });
            }

            return Ok(new { Message = "Phong van updated successfully", Data = result });
        }
        [HttpDelete("delete-phongvan-by-id")]
        public async Task<IActionResult> DeletePhongVan([FromBody] DeletePhongVanCommand command)
        {
            var result = await Mediator.Send(command);
            command.DeletedBy = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (command.DeletedBy.IsNullOrEmpty()) return StatusCode(500, "Cannot get Id from JWT token");


            if (!result)
            {
                return NotFound(new { Message = "Phong van not found or already deleted" });
            }

            return Ok(new { Message = "Phong van deleted successfully" });
        }

        [HttpGet("get-all-comments")]
        [Authorize(Roles = "Staff,Leader")]
        public async Task<ActionResult<GetDetailCommentResponse>> GetAllComments()
        {
            var comments = await Mediator.Send(new GetAllCommentsQuery());
            if (!comments.Any())
            {
                return StatusCode(500, "No comment exists");
            }
            return Ok(comments);
        }

        [HttpGet("get-comment-by-id")]
        [Authorize(Roles = "Staff,Leader")]
        public async Task<IActionResult> GetCommentById([FromQuery] GetCommentByIdQuery query)
        {
            var result = await Mediator.Send(query);
            if (!result.Errors.IsNullOrEmpty()) return StatusCode(500, result.Errors);
            return Ok(result);
        }

        [HttpPost("create-comment")]
        [Authorize(Roles = "Staff,Leader")]
        public async Task<ActionResult<GetDetailCommentResponse>> CreateComment([FromBody] CreateCommentCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Errors.IsNullOrEmpty()) return StatusCode(500, result.Errors);
            return Ok(result);
        }

        [HttpPut("update-comment")]
        [Authorize(Roles = "Staff,Leader")]
        public async Task<ActionResult<GetDetailCommentResponse>> UpdateComment([FromBody] UpdateCommentCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Errors.IsNullOrEmpty()) return StatusCode(500, result.Errors);
            return Ok(result);
        }

        [HttpDelete("delete-comment")]
        [Authorize(Roles = "Staff,Leader")]
        public async Task<IActionResult> DeleteComment([FromBody] DeleteCommentCommand command)
        {
            bool result = await Mediator.Send(command);
            return result ? StatusCode(200, "Delete successfully") : StatusCode(500, "Delete failed");
        }
    }
}
