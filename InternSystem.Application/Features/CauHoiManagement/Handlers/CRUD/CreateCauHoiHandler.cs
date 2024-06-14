using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiManagement.Commands;
using InternSystem.Application.Features.CauHoiManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.CauHoiManagement.Handlers.CRUD
{
    public class CreateCauHoiHandler : IRequestHandler<CreateCauHoiCommand, CreateCauHoiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCauHoiHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateCauHoiResponse> Handle(CreateCauHoiCommand request, CancellationToken cancellationToken)
        {
            CauHoi cauHoi = _mapper.Map<CauHoi>(request);
            cauHoi.LastUpdatedBy = cauHoi.CreatedBy;
            cauHoi.CreatedTime = DateTime.UtcNow.AddHours(7);
            cauHoi.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            cauHoi.IsActive = true;
            cauHoi.IsDelete = false;
            cauHoi = await _unitOfWork.CauHoiRepository.AddAsync(cauHoi);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateCauHoiResponse>(cauHoi);
        }
    }
}

