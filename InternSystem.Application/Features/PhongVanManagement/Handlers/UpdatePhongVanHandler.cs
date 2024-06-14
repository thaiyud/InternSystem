using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.PhongVanManagement.Commands;
using InternSystem.Application.Features.PhongVanManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.PhongVanManagement.Handlers
{
    public class UpdatePhongVanHandler : IRequestHandler<UpdatePhongVanCommand, UpdatePhongVanResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdatePhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdatePhongVanResponse> Handle(UpdatePhongVanCommand request, CancellationToken cancellationToken)
        {
            PhongVan? existingLPV = await _unitOfWork.PhongVanRepository.GetByIdAsync(request.Id);
            if (existingLPV == null || existingLPV.IsDelete == true)
                return new UpdatePhongVanResponse() { Errors = "PhongVan not found" };

            _mapper.Map(request, existingLPV);


            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdatePhongVanResponse>(existingLPV);
        }
    }
}
