using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.KyThucTapManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.KyThucTapManagement.Handlers.CRUD
{
    public class DeleteKyThucTapHandler : IRequestHandler<DeleteKyThucTapCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteKyThucTapHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteKyThucTapCommand request, CancellationToken cancellationToken)
        {
            KyThucTap? existingKTT = await _unitOfWork.KyThucTapRepository.GetByIdAsync(request.Id);
            if (existingKTT == null || existingKTT.IsDelete)
                return false;

            existingKTT.IsActive = false;
            existingKTT.IsDelete = true;
            existingKTT.DeletedBy = request.DeletedBy;
            existingKTT.DeletedTime = DateTimeOffset.Now;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
