using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.LichPhongVanManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Handlers
{
    public class DeleteLichPhongVanHandler : IRequestHandler<DeleteLichPhongVanCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteLichPhongVanHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteLichPhongVanCommand request, CancellationToken cancellationToken)
        {
            LichPhongVan? existingLPV = await _unitOfWork.LichPhongVanRepository.GetByIdAsync(request.Id);
            if (existingLPV == null || existingLPV.IsDelete == true)
                return false;

            existingLPV.DeletedBy = request.DeletedBy;
            existingLPV.DeletedTime = DateTime.UtcNow.AddHours(7);
            existingLPV.IsActive = false;
            existingLPV.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
