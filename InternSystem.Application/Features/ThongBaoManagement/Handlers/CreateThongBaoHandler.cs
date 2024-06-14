using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ThongBaoManagement.Commands;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Handlers
{
    public class CreateThongBaoHandler : IRequestHandler<CreateThongBaoCommand, CreateThongBaoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateThongBaoHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateThongBaoResponse> Handle(CreateThongBaoCommand request, CancellationToken cancellationToken)
        {
            //AspNetUser? existingNguoiNhan = await _unitOfWork.UserRepository.GetByIdAsync(request.IdNguoiNhan);
            //if (existingNguoiNhan== null) return new CreateThongBaoResponse() { Errors = "IdNguoiNhan not found" };

            //AspNetUser? existingNguoiGui = await _unitOfWork.UserRepository.GetByIdAsync(request.IdNguoiGui);
            //if (existingNguoiGui == null) return new CreateThongBaoResponse() { Errors = "IdNguoiGui not found" };

            ThongBao newThongBao = _mapper.Map<ThongBao>(request);
            newThongBao.IsActive = true;
            newThongBao.IsDelete = false;
            newThongBao.CreatedTime = DateTime.UtcNow.AddHours(7);
            newThongBao.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            newThongBao.LastUpdatedBy = request.CreateBy;
            newThongBao.DeletedTime = DateTime.UtcNow.AddYears(7); //DB Does not allow null. Please fix

            newThongBao = await _unitOfWork.ThongBaoRepository.AddAsync(newThongBao);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateThongBaoResponse>(newThongBao);
        }
    }
}
