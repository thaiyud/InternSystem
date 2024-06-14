using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TruongHocManagement.Commands;
using InternSystem.Application.Features.TruongHocManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.TruongHocManagement.Handlers.CRUD
{
    public class CreateTruongHocHandler : IRequestHandler<CreateTruongHocCommand, CreateTruongHocResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateTruongHocHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateTruongHocResponse> Handle(CreateTruongHocCommand request, CancellationToken cancellationToken)
        {
            var existingTruong = await _unitOfWork.TruongHocRepository.GetTruongHocsByTenAsync(request.Ten);
            if (existingTruong.Any())
                return new CreateTruongHocResponse() { Errors = "Duplicate 'Ten'" };

            TruongHoc newTruong = _mapper.Map<TruongHoc>(request);
            newTruong.LastUpdatedTime = DateTimeOffset.Now;
            newTruong.LastUpdatedBy = request.CreatedBy;
            newTruong = await _unitOfWork.TruongHocRepository.AddAsync(newTruong);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CreateTruongHocResponse>(newTruong);

        }
    }
}
