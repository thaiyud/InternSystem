using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Queries;
using MediatR;

namespace InternSystem.Application.Features.CongNgheManagement.Handlers
{
    public class GetAllCongNgheHandler : IRequestHandler<GetAllCongNgheQuery, IEnumerable<GetAllCongNgheResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCongNgheHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetAllCongNgheResponse>> Handle(GetAllCongNgheQuery request, CancellationToken cancellationToken)
        {
            var CongNghe = await _unitOfWork.CongNgheRepository.GetAllASync();
            Console.WriteLine(CongNghe);
            return _mapper.Map<IEnumerable<GetAllCongNgheResponse>>(CongNghe);
        }
    }
}
