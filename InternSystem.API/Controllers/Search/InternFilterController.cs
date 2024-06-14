
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternFilterController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public InternFilterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("FilterInternInfo")]

        public async Task<ActionResult<IEnumerable<InternInfo>>> GetFilteredInternInfo([FromQuery] GetFilteredInternInfoQuery query)
        {
            var result = await Mediator.Send(query);
              return Ok(result);
        }
    }
}
