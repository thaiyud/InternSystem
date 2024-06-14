using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.LichPhongVanManagement.Commands;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Handlers
{
    public class UpdateLichPhongVanHandler : IRequestHandler<UpdateLichPhongVanCommand, UpdateLichPhongVanResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateLichPhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateLichPhongVanResponse> Handle(UpdateLichPhongVanCommand request, CancellationToken cancellationToken)
        {
            LichPhongVan? existingLPV = await _unitOfWork.LichPhongVanRepository.GetByIdAsync(request.Id);
            if (existingLPV == null || existingLPV.IsDelete == true)
                return new UpdateLichPhongVanResponse() { Errors = "LichPhongVan not found" };

            _mapper.Map(request, existingLPV);
           

            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateLichPhongVanResponse>(existingLPV);
        }
    }
}
