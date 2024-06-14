using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.Comunication.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class DeleteUserNhomZaloCommandHandler : IRequestHandler<DeleteUserNhomZaloCommand, DeleteUserNhomZaloResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteUserNhomZaloCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeleteUserNhomZaloResponse> Handle(DeleteUserNhomZaloCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userNhomZalo = await _unitOfWork.UserNhomZaloRepository.GetByIdAsync(request.Id);

            if (userNhomZalo == null)
            {
                return new DeleteUserNhomZaloResponse { IsSuccessful = false };
            }

            userNhomZalo.IsDelete = true;
            userNhomZalo.DeletedBy = currentUserId;
            userNhomZalo.DeletedTime = DateTimeOffset.Now;

            _unitOfWork.UserNhomZaloRepository.UpdateUserNhomZaloAsync(userNhomZalo);
            await _unitOfWork.SaveChangeAsync();

            return new DeleteUserNhomZaloResponse { IsSuccessful = true };
        }
    }
}
