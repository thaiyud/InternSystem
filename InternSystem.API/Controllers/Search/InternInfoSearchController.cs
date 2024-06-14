using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Application.Features.Search.Queries;
using InternSystem.Application.Features.User.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers.Search
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternInfoSearchController : ApiControllerBase
    {
        [HttpGet("interninfo/by-truonghoc-name")]
        public async Task<IActionResult> GetInternInfoByTruongHocName(string truongHocName)
        {
            var query = new GetInternInfoByTruongHocNameQuery(truongHocName);
            var internInfos = await Mediator.Send(query);
            if (internInfos == null || !internInfos.Any())
            {
                return NotFound();
            }
            return Ok(internInfos);
        }

        [HttpPost("interninfo/tu-khoa")]
        public async Task<IActionResult> GetInternInfoByTuKhoa([FromBody] GetInternInfoQuery query)
        {
            var internInfos = await Mediator.Send(query);
            if (internInfos == null || !internInfos.Results.Any())
            {
                return NotFound();
            }
            return Ok(internInfos);
        }
    }
}
