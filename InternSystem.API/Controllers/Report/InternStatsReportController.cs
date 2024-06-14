using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Application.Features.TruongHocManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternSystem.API.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternStatsController : ApiControllerBase
    {
        [HttpGet("GetInternStatsBySchool")]
        public async Task<IActionResult> GetInternStatsBySchool([FromQuery] GetInternStatsBySchoolIdQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
