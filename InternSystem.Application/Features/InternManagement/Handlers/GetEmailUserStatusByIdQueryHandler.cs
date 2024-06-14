using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Handlers
{
    public class GetEmailUserStatusByIdQueryHandler : IRequestHandler<GetEmailUserStatusByIdQuery, GetDetailEmailUserStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmailUserStatusByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetDetailEmailUserStatusResponse> Handle(GetEmailUserStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var existStatus = await _unitOfWork.EmailUserStatusRepository.GetByIdAsync(request.Id);
            if (existStatus == null || existStatus.IsDelete == true)
                return new GetDetailEmailUserStatusResponse() { Errors = "Email user status is not found" };

            return _mapper.Map<GetDetailEmailUserStatusResponse>(existStatus);
        }
    }
}
