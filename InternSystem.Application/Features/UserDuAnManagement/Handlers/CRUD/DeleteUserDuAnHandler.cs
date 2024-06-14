using FluentValidation;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Application.Features.UserDuAnManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserDuAnManagement.Handlers.CRUD
{
    public class DeleteUserDuAnHandler : IRequestHandler<DeleteUserDuAnCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteUserDuAnHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteUserDuAnCommand request, CancellationToken cancellationToken)
        {
            UserDuAn? existingUserDA = await _unitOfWork.UserDuAnRepository.GetByIdAsync(request.Id);
            if (existingUserDA == null || existingUserDA.IsDelete == true)
                return false;

            existingUserDA.DeletedBy = request.DeletedBy;
            existingUserDA.DeletedTime = DateTime.UtcNow.AddHours(7);
            existingUserDA.IsActive = false;
            existingUserDA.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
