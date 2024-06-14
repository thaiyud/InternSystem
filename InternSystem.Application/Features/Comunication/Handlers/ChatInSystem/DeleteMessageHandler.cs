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
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hubContext;

        public DeleteMessageHandler(IUnitOfWork unitOfWork, IHubContext<ChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.MessageRepository.GetByIdAsync(request.MessageId);
            if (message == null)
            {
                throw new ArgumentException("Message not found");
            }

            _unitOfWork.MessageRepository.Remove(message);
            await _unitOfWork.SaveChangeAsync();

            // Notify via SignalR
            await _hubContext.Clients.User(message.IdReceiver).SendAsync("DeleteMessage", request.MessageId);
            await _hubContext.Clients.User(message.IdSender).SendAsync("DeleteMessage", request.MessageId);
            return true;
        }
    }
}
