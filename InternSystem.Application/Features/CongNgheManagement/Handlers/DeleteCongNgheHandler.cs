using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheManagement.Handlers
{
    public class DeleteCongNgheHandler : IRequestHandler<DeleteCongNgheCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCongNgheHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteCongNgheCommand request, CancellationToken cancellationToken)
        {
            CongNghe? existingCN = await _unitOfWork.CongNgheRepository.GetByIdAsync(request.Id);
            if (existingCN == null || existingCN.IsDelete == true)
                return false;
            request.DeletedBy = "current usser";
            existingCN.DeletedBy = request.DeletedBy;
            existingCN.DeletedTime = DateTime.UtcNow.AddHours(7);
            existingCN.IsActive = false;
            existingCN.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
