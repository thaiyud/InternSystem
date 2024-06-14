using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheDuAnManagement.Commands;
using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Handlers
{
    public class UpdateCongNgheDuAnHandler : IRequestHandler<UpdateCongNgheDuAnCommand, UpdateCongNgheDuAnResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateCongNgheDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateCongNgheDuAnResponse> Handle(UpdateCongNgheDuAnCommand request, CancellationToken cancellationToken)
        {
            CongNghe? existingCN = await _unitOfWork.CongNgheRepository.GetByIdAsync(request.IdCongNghe);
            if (existingCN == null || existingCN.IsDelete == true)
                return new UpdateCongNgheDuAnResponse() { Errors = "CongNghe not found" };


            DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.IdDuAn);
            if (existingDA == null || existingDA.IsDelete == true)
                return new UpdateCongNgheDuAnResponse() { Errors = "DuAn not found" };


            CongNgheDuAn? existingCNDA = await _unitOfWork.CongNgheDuAnRepository.GetByIdAsync(request.Id);
            if (existingCNDA == null || existingCNDA.IsDelete == true) return new UpdateCongNgheDuAnResponse() { Errors = "CongNgheDuAn not found" };

            request.LastUpdatedBy = "Current User";
            existingCNDA.LastUpdatedBy = request.LastUpdatedBy;
            existingCNDA.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            existingCNDA = _mapper.Map(request, existingCNDA);


            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateCongNgheDuAnResponse>(existingCNDA);
        }
    }
}
