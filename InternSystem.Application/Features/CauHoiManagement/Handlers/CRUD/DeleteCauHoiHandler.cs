using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiManagement.Commands;
using InternSystem.Application.Features.CauHoiManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiManagement.Handlers.CRUD
{
    public class DeleteCauHoiHandler : IRequestHandler<DeleteCauHoiCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCauHoiHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteCauHoiCommand request, CancellationToken cancellationToken)
        {
            CauHoi? existingCauHoi = await _unitOfWork.CauHoiRepository.GetByIdAsync(request.Id);
            if (existingCauHoi == null || !existingCauHoi.IsActive)
            {
                return false;
            }
            existingCauHoi.DeletedBy = request.DeletedBy;
            existingCauHoi.DeletedTime = DateTime.UtcNow.AddHours(7);
            existingCauHoi.IsActive = false;
            existingCauHoi.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
