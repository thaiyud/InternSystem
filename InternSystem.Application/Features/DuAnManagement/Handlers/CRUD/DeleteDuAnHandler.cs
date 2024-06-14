using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DuAnManagement.Handlers.CRUD
{
    public class DeleteDuAnHandler : IRequestHandler<DeleteDuAnCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteDuAnHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

            public async Task<bool> Handle(DeleteDuAnCommand request, CancellationToken cancellationToken)
            {
                DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.Id);
                if (existingDA == null || existingDA.IsDelete == true)
                    return false;

                existingDA.DeletedBy = request.DeletedBy;
                existingDA.DeletedTime = DateTime.UtcNow.AddHours(7);
                existingDA.IsActive = false;
                existingDA.IsDelete = true;

                await _unitOfWork.SaveChangeAsync();
                return true;
            }
        }
}
