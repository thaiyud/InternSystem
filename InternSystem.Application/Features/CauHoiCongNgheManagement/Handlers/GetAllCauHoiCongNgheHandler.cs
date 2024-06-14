using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Queries;
using MediatR;

namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Handlers
{
    public class GetAllCauHoiCongNgheHandler : IRequestHandler<GetAllCauHoiCongNgheQuery, IEnumerable<GetAllCauHoiCongNgheResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCauHoiCongNgheHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllCauHoiCongNgheResponse>> Handle(GetAllCauHoiCongNgheQuery request, CancellationToken cancellationToken)
        {
            var cauHoiCongNghe = await _unitOfWork.CauHoiCongNgheRepository.GetAllASync();
            return _mapper.Map<IEnumerable<GetAllCauHoiCongNgheResponse>>(cauHoiCongNghe);
        }
    }
}
