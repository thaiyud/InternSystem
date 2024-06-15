using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiManagement.Models;
using InternSystem.Application.Features.CauHoiManagement.Queries;
using MediatR;

namespace InternSystem.Application.Features.CauHoiManagement.Handlers.CRUD
{
    public class GetAllCauHoiHandler : IRequestHandler<GetAllCauHoiQuery, IEnumerable<GetAllCauHoiResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCauHoiHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllCauHoiResponse>> Handle(GetAllCauHoiQuery request, CancellationToken cancellationToken)
        {
            var cauHoi = await _unitOfWork.CauHoiRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAllCauHoiResponse>>(cauHoi);
        }
    }
}
