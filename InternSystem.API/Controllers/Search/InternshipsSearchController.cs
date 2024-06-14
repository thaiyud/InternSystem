using InternSystem.Application.Features.User.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers.Search
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipsSearchController : ApiControllerBase
    {
        [HttpGet("kythuctaps/by-name")]
        public async Task<IActionResult> GetKyThucTapsByName(string ten)
        {
            var query = new GetKyThucTapsByTenQuery(ten);
            var kyThucTaps = await Mediator.Send(query);
            if (kyThucTaps == null || !kyThucTaps.Any())
            {
                return NotFound();
            }
            return Ok(kyThucTaps);
        }
    }
}
