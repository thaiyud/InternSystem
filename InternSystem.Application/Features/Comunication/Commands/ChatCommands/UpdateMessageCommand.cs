using FluentValidation;
using MediatR;

namespace InternSystem.Application.Features.Comunication.Commands.ChatCommands
{
    public class UpdateMessageCommand : IRequest<bool>
    {
        public string MessageId { get; set; }
        public string NewMessageText { get; set; }

        public UpdateMessageCommand(string messageId, string newMessageText)
        {
            MessageId = messageId;
            NewMessageText = newMessageText;
        }
    }

    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty();
            RuleFor(x => x.NewMessageText).NotEmpty();
        }
    }
}
