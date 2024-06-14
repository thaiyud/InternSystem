using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using InternSystem.Application.Features.CongNgheDuAnManagement.Queries;
using MediatR;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Handlers
{
    public class GetAllCongNgheDuAnHandler : IRequestHandler<GetAllCongNgheDuAnQuery, IEnumerable<GetAllCongNgheDuAnResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCongNgheDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetAllCongNgheDuAnResponse>> Handle(GetAllCongNgheDuAnQuery request, CancellationToken cancellationToken)
        {
            var CongNgheDuAn = await _unitOfWork.CongNgheDuAnRepository.GetAllASync();
            Console.WriteLine(CongNgheDuAn);
            return _mapper.Map<IEnumerable<GetAllCongNgheDuAnResponse>>(CongNgheDuAn);
        }
    }
}
