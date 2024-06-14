using FluentValidation;
using MediatR;

namespace InternSystem.Application.Features.Comunication.Commands.ChatCommands
{
    public class SendMessageCommand : IRequest<bool>
    {
        public string IdSender { get; set; }
        public string IdReceiver { get; set; }
        public string MessageText { get; set; }

        public SendMessageCommand(string idSender, string idReceiver, string messageText)
        {
            IdSender = idSender;
            IdReceiver = idReceiver;
            MessageText = messageText;
        }
    }

    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(command => command.IdSender)
                .NotEmpty().WithMessage("Sender ID cannot be empty");

            RuleFor(command => command.IdReceiver)
                .NotEmpty().WithMessage("Receiver ID cannot be empty")
                .NotEqual(command => command.IdSender).WithMessage("Sender and Receiver cannot be the same");

            RuleFor(command => command.MessageText)
                .NotEmpty().WithMessage("Message text cannot be empty");
        }
    }
}
