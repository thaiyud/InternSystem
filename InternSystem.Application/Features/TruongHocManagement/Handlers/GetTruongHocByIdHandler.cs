using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TruongHocManagement.Models;
using InternSystem.Application.Features.TruongHocManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.TruongHocManagement.Handlers
{
    public class GetTruongHocByIdHandler : IRequestHandler<GetTruongHocByIdQuery, GetTruongHocByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTruongHocByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetTruongHocByIdResponse> Handle(GetTruongHocByIdQuery request, CancellationToken cancellationToken)
        {
            TruongHoc? searchResult = await _unitOfWork.TruongHocRepository.GetByIdAsync(request.Id);

            if (searchResult == null || searchResult.IsDelete == true) return new GetTruongHocByIdResponse() { Errors = "Not found" };

            return _mapper.Map<GetTruongHocByIdResponse>(searchResult);
        }
    }
}
