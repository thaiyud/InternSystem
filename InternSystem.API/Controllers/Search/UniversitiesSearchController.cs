using InternSystem.Application.Features.User.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers.Search
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesSearchController : ApiControllerBase
    {
        [HttpGet("truonghocs/by-name")]
        public async Task<IActionResult> GetTruongHocsByTen(string ten)
        {
            var query = new GetTruongHocByTenQuery(ten);
            var truongHocs = await Mediator.Send(query);
            if (truongHocs == null || !truongHocs.Any())
            {
                return NotFound();
            }
            return Ok(truongHocs);
        }
    }
}
