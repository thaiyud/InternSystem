using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.UserDuAnManagement.Commands;
using InternSystem.Application.Features.UserDuAnManagement.Models;
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
    public class CreateUserDuAnHandler : IRequestHandler<CreateUserDuAnCommand, CreateUserDuAnResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateUserDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateUserDuAnResponse> Handle(CreateUserDuAnCommand request, CancellationToken cancellationToken)
        {
            DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId);

            if (existingDA == null) return new CreateUserDuAnResponse() { Errors = "DuAn not found" };

            AspNetUser user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user == null) return new CreateUserDuAnResponse() { Errors = "User not found" };

            UserDuAn newUserDA = _mapper.Map<UserDuAn>(request);
            newUserDA.LastUpdatedBy = newUserDA.CreatedBy;
            newUserDA.CreatedTime = DateTime.UtcNow.AddHours(7);
            newUserDA.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            newUserDA.IsActive = true;
            newUserDA.IsDelete = false;
            newUserDA = await _unitOfWork.UserDuAnRepository.AddAsync(newUserDA);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateUserDuAnResponse>(newUserDA);
        }
    }
}
