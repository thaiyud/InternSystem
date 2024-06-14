using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TruongHocManagement.Commands;
using InternSystem.Application.Features.TruongHocManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.Application.Features.TruongHocManagement.Handlers.CRUD
{
    public class UpdateTruongHocHandler : IRequestHandler<UpdateTruongHocCommand, UpdateTruongHocResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateTruongHocHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateTruongHocResponse> Handle(UpdateTruongHocCommand request, CancellationToken cancellationToken)
        {
            TruongHoc? existingTruongHoc = await _unitOfWork.TruongHocRepository.GetByIdAsync(request.Id);
            if (existingTruongHoc == null || existingTruongHoc.IsDelete == true) return new UpdateTruongHocResponse() { Errors = "'TruongHoc' not found" };

            _mapper.Map(request, existingTruongHoc);
            existingTruongHoc.LastUpdatedTime = DateTime.UtcNow.AddHours(7);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<UpdateTruongHocResponse>(existingTruongHoc);
        }
    }
}
