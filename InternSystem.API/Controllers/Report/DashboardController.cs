using InternSystem.Application.Features.DashboardManage;
using InternSystem.Application.Features.DashboardManage.Models;
using InternSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InternSystem.API.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetAllDashboardResponse>> GetDashboard([FromQuery] GetAllDashboardQuery query)
        {
            var totalInternStudents = await Mediator.Send(query);

            return Ok(totalInternStudents);
        }
    }
}
