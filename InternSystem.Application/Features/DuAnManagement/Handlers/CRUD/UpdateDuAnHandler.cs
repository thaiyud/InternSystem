using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DuAnManagement.Handlers.CRUD
{
    public class UpdateDuAnHandler : IRequestHandler<UpdateDuAnCommand, UpdateDuAnResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateDuAnResponse> Handle(UpdateDuAnCommand request, CancellationToken cancellationToken)
        {
            DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.Id);
            if (existingDA == null || existingDA.IsDelete == true) return new UpdateDuAnResponse() { Errors = "DuAn not found" };

            if (!request.LeaderId.IsNullOrEmpty())
            {
                AspNetUser? leader = await _unitOfWork.UserRepository.GetByIdAsync(request.LeaderId!);
                if (leader == null) return new UpdateDuAnResponse() { Errors = "Leader not found" };
            }

            existingDA = _mapper.Map(request, existingDA);
            if (existingDA.ThoiGianBatDau > existingDA.ThoiGianKetThuc || existingDA.ThoiGianKetThuc < existingDA.ThoiGianBatDau)
                return new UpdateDuAnResponse() { Errors = "ThoiGianBatDau must be before/earlier than ThoiGianKetThuc" };

            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateDuAnResponse>(existingDA);
        }
    }
}
