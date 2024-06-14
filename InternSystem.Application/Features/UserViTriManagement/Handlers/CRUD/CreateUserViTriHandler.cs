using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSystem.Application.Features.UserViTriManagement.Models;
using InternSystem.Application.Features.UserViTriManagement.Commands;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.UserViTriManagement.Handlers.CRUD
{
    public class CreateUserViTriHandler : IRequestHandler<CreateUserViTriCommand, CreateUserViTriResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateUserViTriHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateUserViTriResponse> Handle(CreateUserViTriCommand request, CancellationToken cancellationToken)
        {
            ViTri? existingViTri = await _unitOfWork.ViTriRepository.GetByIdAsync(request.IdViTri);

            if (existingViTri == null) return new CreateUserViTriResponse() { Errors = "ViTri not found" };

            AspNetUser? existingUser = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (existingUser == null) return new CreateUserViTriResponse() { Errors = "User not found" };

            UserViTri newUserViTri = _mapper.Map<UserViTri>(request);
            newUserViTri.LastUpdatedBy = newUserViTri.CreatedBy;
            newUserViTri.CreatedTime = DateTime.UtcNow.AddHours(7);
            newUserViTri.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            newUserViTri.IsActive = true;
            newUserViTri.IsDelete = false;
            newUserViTri = await _unitOfWork.UserViTriRepository.AddAsync(newUserViTri);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateUserViTriResponse>(newUserViTri);
        }
    }
}
