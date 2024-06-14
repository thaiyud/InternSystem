using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheManagement.Commands;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheManagement.Handlers
{
    public class UpdateCongNgheHandler : IRequestHandler<UpdateCongNgheCommand, UpdateCongNgheResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateCongNgheHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateCongNgheResponse> Handle(UpdateCongNgheCommand request, CancellationToken cancellationToken)
        {
            CongNghe? existingCN = await _unitOfWork.CongNgheRepository.GetByIdAsync(request.Id);
            if (existingCN == null || existingCN.IsDelete == true) return new UpdateCongNgheResponse() { Errors = "CongNghe not found" };

            //if (!request.LeaderId.IsNullOrEmpty())
            //{
            //    AspNetUser? leader = await _unitOfWork.UserRepository.GetByIdAsync(request.LeaderId!);
            //    if (leader == null) return new UpdateCongNgheResponse() { Errors = "Leader not found" };
            //}
            request.LastUpdatedBy = "Current user";
            existingCN.LastUpdatedBy = request.LastUpdatedBy;
            existingCN.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            existingCN = _mapper.Map(request, existingCN);


            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateCongNgheResponse>(existingCN);
        }
    }
}
