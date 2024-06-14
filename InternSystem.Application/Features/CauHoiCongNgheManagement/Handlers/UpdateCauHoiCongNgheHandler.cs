using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Commands;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Handlers
{
    public class UpdateCauHoiCongNgheHandler : IRequestHandler<UpdateCauHoiCongNgheCommand, UpdateCauHoiCongNgheResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateCauHoiCongNgheHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateCauHoiCongNgheResponse> Handle(UpdateCauHoiCongNgheCommand request, CancellationToken cancellationToken)
        {
            CauHoiCongNghe? cauHoiCongNghe = await _unitOfWork.CauHoiCongNgheRepository.GetByIdAsync(request.Id);
            if (cauHoiCongNghe == null || !cauHoiCongNghe.IsActive)
            {
                return new UpdateCauHoiCongNgheResponse() { Errors = "Cau hoi cong nghe not found" };
            }
            CauHoi? cauHoi = await _unitOfWork.CauHoiRepository.GetByIdAsync(request.IdCauHoi);
            if (cauHoi == null)
            {
                return new UpdateCauHoiCongNgheResponse { Errors = "Cau hoi not found" };
            }
            CongNghe? congNghe = await _unitOfWork.CongNgheRepository.GetByIdAsync(request.IdCongNghe);
            if (congNghe == null)
            {
                return new UpdateCauHoiCongNgheResponse { Errors = "Cong nghe not found" };
            }

            cauHoiCongNghe = _mapper.Map(request, cauHoiCongNghe);
            cauHoiCongNghe.LastUpdatedTime = DateTime.UtcNow.AddHours(7);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<UpdateCauHoiCongNgheResponse>(cauHoiCongNghe);
        }
    }
}
