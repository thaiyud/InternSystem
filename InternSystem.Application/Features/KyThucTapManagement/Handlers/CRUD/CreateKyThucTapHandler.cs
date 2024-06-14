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
    public class CreateKyThucTapHandler : IRequestHandler<CreateKyThucTapCommand, CreateKyThucTapResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateKyThucTapHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateKyThucTapResponse> Handle(CreateKyThucTapCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<KyThucTap> existingKTT = await _unitOfWork.KyThucTapRepository.GetKyThucTapsByNameAsync(request.Ten);
            if (existingKTT.Any())
                return new CreateKyThucTapResponse() { Errors = "Duplicate KyThucTap name" };

            TruongHoc? existingTruong = await _unitOfWork.TruongHocRepository.GetByIdAsync(request.IdTruong);
            if (existingTruong == null || existingTruong.IsDelete || !existingTruong.IsActive)
                return new CreateKyThucTapResponse()
                { Errors = "IdTruong not found" };

            KyThucTap newKTT = _mapper.Map<KyThucTap>(request);
            newKTT.LastUpdatedTime = newKTT.CreatedTime;
            newKTT.LastUpdatedBy = newKTT.CreatedBy;

            newKTT = await _unitOfWork.KyThucTapRepository.AddAsync(newKTT);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CreateKyThucTapResponse>(newKTT);
        }
    }
}
