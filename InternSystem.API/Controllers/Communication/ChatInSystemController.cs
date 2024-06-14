using FluentValidation;
using InternSystem.Application.Features.Comunication.Commands.ChatCommands;
using InternSystem.Application.Features.Message.Models;
using InternSystem.Application.Features.Message.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InternSystem.API.Controllers.Communication
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatInSystemController : ControllerBase
    {


        private readonly IMediator _mediator;

        public ChatInSystemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get_Message_History")]
        public async Task<ActionResult<List<GetMessageHistoryResponse>>> GetMessageHistory([FromQuery] string idSender, [FromQuery] string idReceiver)
        {
            var query = new GetMessageHistoryQuery
            {
                IdSender = idSender,
                IdReceiver = idReceiver
            };
            var messages = await _mediator.Send(query);
            return Ok(messages);
        }

        [HttpPost("Send_Message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            var validator = new SendMessageCommandValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok("Message sent successfully");
                }
                else
                {
                    return StatusCode(500, "An error occurred while sending the message");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        [HttpDelete("Delete_Message/{messageId}")]
        public async Task<IActionResult> DeleteMessage(string messageId)
        {
            var command = new DeleteMessageCommand(messageId);
            var validator = new DeleteMessageCommandValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok("Message deleted successfully");
                }
                else
                {
                    return StatusCode(500, "An error occurred while deleting the message");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPut("Update_Message")]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageCommand command)
        {
            var validator = new UpdateMessageCommandValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok("Message updated successfully");
                }
                else
                {
                    return StatusCode(500, "An error occurred while updating the message");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
    
}
