using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheDuAnManagement.Commands;
using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Handlers
{
    public class CreateCongNgheDuAnHandler : IRequestHandler<CreateCongNgheDuAnCommand, CreateCongNgheDuAnResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateCongNgheDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateCongNgheDuAnResponse> Handle(CreateCongNgheDuAnCommand request, CancellationToken cancellationToken)
        {
            CongNghe? existingCN = await _unitOfWork.CongNgheRepository.GetByIdAsync(request.IdCongNghe);
            if (existingCN == null || existingCN.IsDelete == true)
                return new CreateCongNgheDuAnResponse() { Errors = "CongNghe not found" };


            DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.IdDuAn);
            if (existingDA == null || existingDA.IsDelete == true)
                return new CreateCongNgheDuAnResponse() { Errors = "DuAn not found" };

            request.CreatedBy = "Current user";
            CongNgheDuAn newCNDA = _mapper.Map<CongNgheDuAn>(request);
            newCNDA.LastUpdatedBy = newCNDA.CreatedBy;
            newCNDA.CreatedTime = DateTime.UtcNow.AddHours(7);
            newCNDA.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            newCNDA.DeletedTime = DateTime.UtcNow.AddHours(7);
            newCNDA.IsActive = true;
            newCNDA.IsDelete = false;
            newCNDA = await _unitOfWork.CongNgheDuAnRepository.AddAsync(newCNDA);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateCongNgheDuAnResponse>(newCNDA);
        }
    }
}
