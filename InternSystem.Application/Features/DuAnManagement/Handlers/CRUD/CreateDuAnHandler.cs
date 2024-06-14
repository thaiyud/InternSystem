using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Application.Features.DuAnManagement.Models;
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
    public class CreateDuAnHandler : IRequestHandler<CreateDuAnCommand, CreateDuAnResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateDuAnResponse> Handle(CreateDuAnCommand request, CancellationToken cancellationToken)
        {
            DuAn? existingDA = _unitOfWork.DuAnRepository.GetAllASync().Result.AsQueryable()
                .FirstOrDefault(d => d.Ten.Equals(request.Ten));

            if (existingDA != null) return new CreateDuAnResponse() { Errors = "Duplicate DuAn name" };
            DuAn newDA = _mapper.Map<DuAn>(request);
            newDA.LastUpdatedBy = newDA.CreatedBy;
            newDA.CreatedTime = DateTime.UtcNow.AddHours(7);
            newDA.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            newDA.IsActive = true;
            newDA.IsDelete = false;
            newDA = await _unitOfWork.DuAnRepository.AddAsync(newDA);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateDuAnResponse>(newDA);
        }
    }
}
