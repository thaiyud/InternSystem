using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Models;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetKyThucTapsByTenQueryHandler : IRequestHandler<GetKyThucTapsByTenQuery, IEnumerable<GetKyThucTapsByNameResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetKyThucTapsByTenQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetKyThucTapsByNameResponse>> Handle(GetKyThucTapsByTenQuery request, CancellationToken cancellationToken)
        {
            var kyThucTaps = await _unitOfWork.KyThucTapRepository.GetKyThucTapsByNameAsync(request.Ten);
            Console.WriteLine(kyThucTaps);
            return _mapper.Map<IEnumerable<GetKyThucTapsByNameResponse>>(kyThucTaps);
        }
    }
}
