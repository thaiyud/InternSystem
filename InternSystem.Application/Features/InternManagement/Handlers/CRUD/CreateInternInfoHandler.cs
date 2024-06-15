using AutoMapper;
using FluentValidation;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Commands;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace InternSystem.Application.Features.InternManagement.Handlers.CRUD
{

    public class CreateInternInfoHandler : IRequestHandler<CreateInternInfoCommand, CreateInternInfoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateInternInfoHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateInternInfoResponse> Handle(CreateInternInfoCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.IsNullOrEmpty())
            {
                InternInfo? existingIntern = _unitOfWork.InternInfoRepository.GetAllAsync().Result.AsQueryable()
               .Where(b => b.UserId.Equals(request.UserId)).FirstOrDefault();

                if (existingIntern != null)
                    return new CreateInternInfoResponse() { Errors = "User already has an intern profile." };
            }

 
            TruongHoc? existingTruong = await _unitOfWork.TruongHocRepository.GetByIdAsync(request.IdTruong);
            if (existingTruong == null || existingTruong.IsDelete == true)
                return new CreateInternInfoResponse() { Errors = "TruongHoc not found" };

            if (request.KyThucTapId.HasValue)
            {
                KyThucTap? existingKTT = await _unitOfWork.KyThucTapRepository.GetByIdAsync(request.KyThucTapId);
                if (existingKTT == null || existingKTT.IsDelete)
                    return new CreateInternInfoResponse()
                    { Errors = "KiThucTap not found" };
            }

            if (request.DuAnId.HasValue)
            {
                DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId);
                if (existingDA == null || existingDA.IsDelete == true)
                    return new CreateInternInfoResponse() { Errors = "DuAn not found" };
            }

            InternInfo newIntern = _mapper.Map<InternInfo>(request);
            newIntern.LastUpdatedBy = request.CreatedBy; // null not allowed, please fix
            newIntern.LastUpdatedTime = newIntern.CreatedTime;
            newIntern.DeletedTime = DateTime.UtcNow.AddYears(7); //null not allowed. please fix

            newIntern = await _unitOfWork.InternInfoRepository.AddAsync(newIntern);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CreateInternInfoResponse>(newIntern);
        }
    }
}
