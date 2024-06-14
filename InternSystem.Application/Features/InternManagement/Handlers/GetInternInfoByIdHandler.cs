using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Handlers
{
    public class GetInternInfoByIdHandler : IRequestHandler<GetInternInfoByIdQuery, GetInternInfoByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInternInfoByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetInternInfoByIdResponse> Handle(GetInternInfoByIdQuery request, CancellationToken cancellationToken)
        {
            InternInfo? searchResult = await _unitOfWork.InternInfoRepository.GetByIdAsync(request.Id);
            if (searchResult == null || searchResult.IsDelete) 
                return new GetInternInfoByIdResponse() { Errors = "Not found" };

            return _mapper.Map<GetInternInfoByIdResponse>(searchResult);
        }
    }
}
