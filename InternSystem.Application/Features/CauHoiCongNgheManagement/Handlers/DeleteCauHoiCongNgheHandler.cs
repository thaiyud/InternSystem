using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Handlers
{
    public class DeleteCauHoiCongNgheHandler : IRequestHandler<DeleteCauHoiCongNgheCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteCauHoiCongNgheHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteCauHoiCongNgheCommand request, CancellationToken cancellationToken)
        {
            CauHoiCongNghe? cauHoiCongNghe = await _unitOfWork.CauHoiCongNgheRepository.GetByIdAsync(request.Id);
            if(cauHoiCongNghe == null || !cauHoiCongNghe.IsActive)
            {
                return false;
            }
            cauHoiCongNghe.DeletedBy = request.DeletedBy;
            cauHoiCongNghe.DeletedTime = DateTime.UtcNow.AddHours(7);
            cauHoiCongNghe.IsActive = false;
            cauHoiCongNghe.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
