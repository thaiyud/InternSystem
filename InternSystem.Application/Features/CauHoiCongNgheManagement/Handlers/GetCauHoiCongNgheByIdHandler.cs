using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;


namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Handlers
{
    public class GetCauHoiCongNgheByIdHandler : IRequestHandler<GetCauHoiCongNgheByIdQuery, GetCauHoiCongNgheByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCauHoiCongNgheByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCauHoiCongNgheByIdResponse> Handle(GetCauHoiCongNgheByIdQuery request, CancellationToken cancellationToken)
        {
            CauHoiCongNghe? cauHoiCongNghe = await _unitOfWork.CauHoiCongNgheRepository.GetByIdAsync(request.Id);
            if (cauHoiCongNghe == null || !cauHoiCongNghe.IsActive)
            {
                return new GetCauHoiCongNgheByIdResponse() { Errors = "Cau hoi cong nghe not found" };
            }

            return _mapper.Map<GetCauHoiCongNgheByIdResponse>(cauHoiCongNghe);
        }
    }
}
