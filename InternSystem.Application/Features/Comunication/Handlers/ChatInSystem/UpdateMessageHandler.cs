using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Commands.ChatCommands;
using InternSystem.Application.Features.Message.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Message.Handlers
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hubContext;

        public UpdateMessageHandler(IUnitOfWork unitOfWork, IHubContext<ChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<bool> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.MessageRepository.GetByIdAsync(request.MessageId);
            if (message == null)
            {
                throw new ArgumentException("Message not found");
            }

            message.MessageText = request.NewMessageText;
            _unitOfWork.MessageRepository.UpdateMessageAsync(message);
            await _unitOfWork.SaveChangeAsync();

            // Notify via SignalR
            await _hubContext.Clients.User(message.IdReceiver).SendAsync("UpdateMessage", request.MessageId, request.NewMessageText);
            await _hubContext.Clients.User(message.IdSender).SendAsync("UpdateMessage", request.MessageId, request.NewMessageText);
            return true;
        }
    }
}
