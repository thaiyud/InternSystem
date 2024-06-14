using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ViTriManagement.Commands;
using InternSystem.Application.Features.ViTriManagement.Handlers.CRUD;
using InternSystem.Application.Features.ViTriManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.ViTriManagement.Handlers.CRUD
{
    public class DeleteUserViTriHandler : IRequestHandler<DeleteViTriCommand, bool>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteUserViTriHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteViTriCommand request, CancellationToken cancellationToken)
        {
            ViTri? existingViTri = await _unitOfWork.ViTriRepository.GetByIdAsync(request.Id);
            if (existingViTri == null || existingViTri.IsDelete == true)
                return false;

            existingViTri.DeletedBy = request.DeletedBy;
            existingViTri.DeletedTime = DateTime.UtcNow.AddHours(7);
            existingViTri.IsActive = false;
            existingViTri.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
