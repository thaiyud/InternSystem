using InternSystem.Application.Features.User.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternReportController : ApiControllerBase
    {
        [HttpGet("total-interns-by-day-to-day")]
        public async Task<IActionResult> GetTotalInternStudents(DateTime startDate, DateTime endDate)
        {
            var query = new GetTotalInternStudentsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var totalInternStudents = await Mediator.Send(query);

            return Ok(totalInternStudents);
        }
    }
}
