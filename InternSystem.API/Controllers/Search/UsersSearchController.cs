using InternSystem.Application.Features.User.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers.Search
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersSearchController : ApiControllerBase
    {
        [HttpGet("users/by-name")]
        public async Task<IActionResult> GetUsersByHoVaTen(string hoVaTen)
        {
            var query = new GetUsersByHoVaTenQuery(hoVaTen);
            var users = await Mediator.Send(query);
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}
