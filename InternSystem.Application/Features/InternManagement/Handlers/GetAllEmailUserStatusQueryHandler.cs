using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Handlers
{
    public class GetAllEmailUserStatusQueryHandler : IRequestHandler<GetAllEmailUserStatusQuery, IEnumerable<GetDetailEmailUserStatusResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEmailUserStatusQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDetailEmailUserStatusResponse>> Handle(GetAllEmailUserStatusQuery request, CancellationToken cancellationToken)
        {
            var status = await _unitOfWork.EmailUserStatusRepository.GetAllAsync();
            var filteredStatus = status.Where(c => c.IsActive && !c.IsDelete);
            return _mapper.Map<IEnumerable<GetDetailEmailUserStatusResponse>>(filteredStatus);
        }
    }
}
