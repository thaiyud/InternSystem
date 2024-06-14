using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Commands;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Handlers
{
    public class CreateCauHoiCongNgheHandler : IRequestHandler<CreateCauHoiCongNgheCommand, CreateCauHoiCongNgheResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateCauHoiCongNgheHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateCauHoiCongNgheResponse> Handle(CreateCauHoiCongNgheCommand request, CancellationToken cancellationToken)
        {
            CauHoi? cauHoi = await _unitOfWork.CauHoiRepository.GetByIdAsync(request.IdCauHoi);
            if (cauHoi == null)
            {
                return new CreateCauHoiCongNgheResponse { Errors = "Cau hoi not found" };
            }
            CongNghe? congNghe = await _unitOfWork.CongNgheRepository.GetByIdAsync(request.IdCongNghe);
            if (congNghe == null)
            {
                return new CreateCauHoiCongNgheResponse { Errors = "Cong nghe not found" };
            }
            CauHoiCongNghe? cauHoiCongNghe = _mapper.Map<CauHoiCongNghe>(request);
            cauHoiCongNghe.LastUpdatedBy = cauHoiCongNghe.CreatedBy;
            cauHoiCongNghe.CreatedTime = DateTime.UtcNow.AddHours(7);
            cauHoiCongNghe.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            cauHoiCongNghe.IsActive = true;
            cauHoiCongNghe.IsDelete = false;
            cauHoiCongNghe = await _unitOfWork.CauHoiCongNgheRepository.AddAsync(cauHoiCongNghe);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateCauHoiCongNgheResponse>(cauHoiCongNghe);
        }
    }
}
