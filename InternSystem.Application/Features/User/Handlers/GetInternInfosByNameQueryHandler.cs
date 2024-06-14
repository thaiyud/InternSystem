using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Search.Models;
using InternSystem.Application.Features.Search.Queries;
using MediatR;


namespace InternSystem.Application.Features.User.Handlers
{
    public class GetInternInfosByNameQueryHandler : IRequestHandler<GetInternInfoByTruongHocNameQuery, IEnumerable<GetInternInfoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInternInfosByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetInternInfoResponse>> Handle(GetInternInfoByTruongHocNameQuery request, CancellationToken cancellationToken)
        {
            var internInfos = await _unitOfWork.InternInfoRepository.GetInternInfoByTenTruongHocAsync(request.TruongHocName);
            // Logging should be done with a proper logging framework
            // Console.WriteLine(internInfos);

            return _mapper.Map<IEnumerable<GetInternInfoResponse>>(internInfos);
        }
    }
}