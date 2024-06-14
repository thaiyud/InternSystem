using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ViTriManagement.Commands;
using InternSystem.Application.Features.ViTriManagement.Models;
using InternSystem.Application.Features.ViTriManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.ViTriManagement.Handlers.CRUD
{
    public class CreateUserViTriHandler : IRequestHandler<CreateViTriCommand, CreateViTriResponse>
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

        public async Task<CreateViTriResponse> Handle(CreateViTriCommand request, CancellationToken cancellationToken)
        {
            DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId);

            if (existingDA == null) return new CreateViTriResponse() { Errors = "DuAn not found" };


            ViTri newViTri = _mapper.Map<ViTri>(request);
            newViTri.LastUpdatedBy = newViTri.CreatedBy;
            newViTri.CreatedTime = DateTime.UtcNow.AddHours(7);
            newViTri.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            newViTri.IsActive = true;
            newViTri.IsDelete = false;
            newViTri = await _unitOfWork.ViTriRepository.AddAsync(newViTri);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateViTriResponse>(newViTri);
        }
    }
}
