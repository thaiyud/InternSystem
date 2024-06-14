using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.Comunication.Queries;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternSystem.API.Controllers.Communication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNhomZaloController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserNhomZaloController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserToNhomZalo([FromBody] AddUserToNhomZaloCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid command.");
            }

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("User successfully added to NhomZalo.");
            }

            return BadRequest("Failed to add user to NhomZalo.");
        }

        [HttpGet(Name = nameof(GetAllUserNhomZalos))]
        public async Task<IActionResult> GetAllUserNhomZalos()
        {
            var query = new GetAllUserNhomZaloQuery();
            var response = await _mediator.Send(query);
            if (response == null || !response.Any())
            {
                return NotFound("No UserNhomZalos found.");
            }
            return Ok(response);
        }

        [HttpGet("{id}", Name = nameof(GetUserNhomZaloById))]
        public async Task<IActionResult> GetUserNhomZaloById(int id)
        {
            var query = new GetUserNhomZaloByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound($"UserNhomZalo with id {id} not found.");
            }

            return Ok(result);
        }

        [HttpPut("{id}", Name = nameof(UpdateUserNhomZalo))]
        public async Task<IActionResult> UpdateUserNhomZalo(int id, [FromBody] UpdateUserNhomZaloCommand command)
        {
            var wrapper = new UpdateUserNhomZaloCommandWrapper
            {
                Id = id,
                Command = command
            };

            var response = await _mediator.Send(wrapper);
            if (response.ErrorMessage != null)
            {
                return NotFound($"UserNhomZalo with id {id} not found.");
            }

            return Ok(response);
        }

        [HttpDelete("{id}", Name = nameof(DeleteUserNhomZalo))]
        public async Task<IActionResult> DeleteUserNhomZalo(int id)
        {
            var command = new DeleteUserNhomZaloCommand { Id = id };
            var response = await _mediator.Send(command);

            if (!response.IsSuccessful)
            {
                return NotFound($"UserNhomZalo with id {id} not found.");
            }

            return Ok(new { Message = "Delete successfully" });
        }
    }
}
