using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ThongBaoManagement.Commands;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Handlers
{
    public class DeleteThongBaoHandler : IRequestHandler<DeleteThongBaoCommand, DeleteThongBaoResponse>
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        public DeleteThongBaoHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            HttpContextAccessor = httpContextAccessor;
        }

        public async Task<DeleteThongBaoResponse> Handle(DeleteThongBaoCommand request, CancellationToken cancellationToken)
        {
            ThongBao? existingTB = await _unitOfWork.ThongBaoRepository.GetByIdAsync(request.Id);
            if (existingTB == null) return new DeleteThongBaoResponse() { Errors = "Id not found" };

            existingTB.DeletedTime = DateTime.Now.AddHours(7);
            existingTB.IsActive = false;
            existingTB.IsDelete = true;
            await _unitOfWork.SaveChangeAsync();

            return new DeleteThongBaoResponse();
        }
    }
}
