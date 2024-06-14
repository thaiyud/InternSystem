using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.UserViTriManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserViTriManagement.Handlers.CRUD
{
    public class DeleteUserViTriHandler : IRequestHandler<DeleteUserViTriCommand, bool>
    {
        // bi gi vay 
        // tui update-dâtbase mà nó kh vào database cua tui 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteUserViTriHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteUserViTriCommand request, CancellationToken cancellationToken)
        {
            UserViTri? existingUserViTri = await _unitOfWork.UserViTriRepository.GetByIdAsync(request.Id);
            if (existingUserViTri == null || existingUserViTri.IsDelete == true)
                return false;

            existingUserViTri.DeletedBy = request.DeletedBy;
            existingUserViTri.DeletedTime = DateTime.UtcNow.AddHours(7);
            existingUserViTri.IsActive = false;
            existingUserViTri.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
