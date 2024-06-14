﻿using FluentValidation;
using MediatR;

namespace InternSystem.Application.Features.Comunication.Commands.ChatCommands
{
    public class DeleteMessageCommand : IRequest<bool>
    {
        public string MessageId { get; set; }

        public DeleteMessageCommand(string messageId)
        {
            MessageId = messageId;
        }
    }

    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty();
        }
    }
}
