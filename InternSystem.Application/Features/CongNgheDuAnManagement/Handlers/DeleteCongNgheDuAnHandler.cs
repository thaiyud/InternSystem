using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheDuAnManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Handlers
{
    public class DeleteCongNgheDuAnHandler : IRequestHandler<DeleteCongNgheDuAnCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCongNgheDuAnHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteCongNgheDuAnCommand request, CancellationToken cancellationToken)
        {

            CongNgheDuAn? existingCNDN = await _unitOfWork.CongNgheDuAnRepository.GetByIdAsync(request.Id);
            if (existingCNDN == null || existingCNDN.IsDelete == true)
                return false;
            request.DeletedBy = "Curent user";
            existingCNDN.DeletedBy = request.DeletedBy;
            existingCNDN.DeletedTime = DateTime.UtcNow.AddHours(7);
            existingCNDN.IsActive = false;
            existingCNDN.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
