using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Commands.ChatCommands;
using InternSystem.Application.Features.Message.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Message.Handlers
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public SendMessageHandler(IUnitOfWork unitOfWork, UserManager<AspNetUser> userManager, IHubContext<ChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            // Validate users exist
            var sender = await _userManager.FindByIdAsync(request.IdSender);
            if (sender == null)
            {
                throw new ArgumentException("Sender not found");
            }

            var receiver = await _userManager.FindByIdAsync(request.IdReceiver);
            if (receiver == null)
            {
                throw new ArgumentException("Receiver not found");
            }

            // Validate sender and receiver are not the same
            if (request.IdSender == request.IdReceiver)
            {
                throw new ArgumentException("Sender and receiver cannot be the same");
            }

            var message = new Domain.Entities.Message
            {
                Id = Guid.NewGuid().ToString(),
                IdSender = request.IdSender,
                IdReceiver = request.IdReceiver,
                MessageText = request.MessageText,
                Timestamp = DateTime.UtcNow
            };

            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.SaveChangeAsync();

            // Notify the recipient via SignalR
            await _hubContext.Clients.User(request.IdReceiver).SendAsync("ReceiveMessage", message.IdSender, message.MessageText);

            return true;
        }
    }
}
