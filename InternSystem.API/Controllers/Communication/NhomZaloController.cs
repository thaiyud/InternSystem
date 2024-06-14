using AutoMapper;
using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.Comunication.Queries;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternSystem.API.Controllers.Communication
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomZaloController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NhomZaloController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost(Name = nameof(CreateNhomZalo))]
        public async Task<IActionResult> CreateNhomZalo([FromBody] CreateNhomZaloCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == null)
            {
                return BadRequest("Failed to create NhomZalo.");
            }
            return CreatedAtAction(nameof(GetNhomZaloById), new { id = response.Id }, response);
        }

        [HttpPut("{id}", Name = nameof(UpdateNhomZalo))]
        public async Task<IActionResult> UpdateNhomZalo(int id, [FromBody] UpdateNhomZaloCommand command)
        {
            var wrapper = new UpdateNhomZaloCommandWrapper
            {
                Id = id,
                Command = command
            };

            var response = await _mediator.Send(wrapper);
            if (response == null)
            {
                return NotFound($"NhomZalo with id {id} not found.");
            }

            return Ok(response);
        }

        [HttpDelete("{id}", Name = nameof(DeleteNhomZalo))]
        public async Task<IActionResult> DeleteNhomZalo(int id)
        {
            var command = new DeleteNhomZaloCommand { Id = id };
            var response = await _mediator.Send(command);
            if (!response.IsSuccessful)
            {
                return NotFound($"NhomZalo with id {id} not found.");
            }

            return Ok(new { Message = "Delete successfully" });
        }

        [HttpGet(Name = nameof(GetAllNhomZalos))]
        public async Task<IActionResult> GetAllNhomZalos()
        {
            var query = new GetAllNhomZaloQuery();
            var response = await _mediator.Send(query);
            if (response == null || !response.Any())
            {
                return NotFound("No NhomZalos found.");
            }
            return Ok(response);
        }

        [HttpGet("{id}", Name = nameof(GetNhomZaloById))]
        public async Task<IActionResult> GetNhomZaloById(int id)
        {
            var query = new GetNhomZaloByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound($"NhomZalo with id {id} not found.");
            }

            return Ok(result);
        }
    }
}
