using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.Comunication.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class DeleteNhomZaloCommandHandler : IRequestHandler<DeleteNhomZaloCommand, DeleteNhomZaloResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteNhomZaloCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeleteNhomZaloResponse> Handle(DeleteNhomZaloCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nhomZalo = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.Id);

            if (nhomZalo == null)
            {
                return new DeleteNhomZaloResponse { IsSuccessful = false };
            }

            nhomZalo.IsDelete = true;
            nhomZalo.DeletedBy = currentUserId;
            nhomZalo.DeletedTime = DateTimeOffset.Now;

            _unitOfWork.NhomZaloRepository.UpdateNhomZaloAsync(nhomZalo);
            await _unitOfWork.SaveChangeAsync();

            return new DeleteNhomZaloResponse { IsSuccessful = true };
        }
    }
}
