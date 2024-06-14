using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.KyThucTapManagement.Commands;
using InternSystem.Application.Features.KyThucTapManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.KyThucTapManagement.Handlers.CRUD
{
    public class UpdateKyThucTapHandler : IRequestHandler<UpdateKyThucTapCommand, UpdateKyThucTapResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateKyThucTapHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateKyThucTapResponse> Handle(UpdateKyThucTapCommand request, CancellationToken cancellationToken)
        {
            KyThucTap? existingKTT = await _unitOfWork.KyThucTapRepository.GetByIdAsync(request.Id);
            if (existingKTT == null || existingKTT.IsDelete || !existingKTT.IsActive)
                return new UpdateKyThucTapResponse() { Errors = "KyThucTap not found" };

            TruongHoc? existingTruong = await _unitOfWork.TruongHocRepository.GetByIdAsync(request.IdTruong);
            if (existingTruong == null || existingTruong.IsDelete || !existingTruong.IsActive)
                return new UpdateKyThucTapResponse() { Errors = "IdTruong not found" };

            _mapper.Map(request, existingKTT);
            existingKTT.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateKyThucTapResponse>(existingKTT);
        }
    }
}
