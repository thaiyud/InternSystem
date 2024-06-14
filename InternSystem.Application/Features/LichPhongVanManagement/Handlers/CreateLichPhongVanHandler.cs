using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.LichPhongVanManagement.Commands;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Handlers
{
    public class CreateLichPhongVanHandler : IRequestHandler<CreateLichPhongVanCommand, CreateLichPhongVanResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateLichPhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateLichPhongVanResponse> Handle(CreateLichPhongVanCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra xem lịch phỏng vấn đã tồn tại hay chưa
           /* LichPhongVan? existingLichPhongVan = await _unitOfWork.LichPhongVanRepository.GetAllASync()
                .Result.AsQueryable()
                .FirstOrDefaultAsync(l => l.ThoiGianPhongVan == request.ThoiGianPhongVan && l.DiaDiemPhongVan == request.DiaDiemPhongVan, cancellationToken);*/

           /* if (existingLichPhongVan != null) return new CreateLichPhongVanResponse() { Errors = "Duplicate interview schedule" };
*/
/*            // Kiểm tra xem người phỏng vấn có tồn tại hay không
            AspNetUser nguoiPhongVan = await _unitOfWork.UserRepository.GetByIdAsync(request.IdNguoiPhongVan);
            if (nguoiPhongVan == null) return new CreateLichPhongVanResponse() { Errors = "Interviewer not found" };

            // Kiểm tra xem người được phỏng vấn có tồn tại hay không
            InternInfo nguoiDuocPhongVan = await _unitOfWork.InternInfoRepository.GetByIdAsync(request.IdNguoiDuocPhongVan);
            if (nguoiDuocPhongVan == null) return new CreateLichPhongVanResponse() { Errors = "Interviewee not found" };*/

            // Tạo lịch phỏng vấn mới
            LichPhongVan newLichPhongVan = _mapper.Map<LichPhongVan>(request);
            newLichPhongVan.LastUpdatedBy = newLichPhongVan.CreatedBy;
            newLichPhongVan.CreatedTime = DateTime.UtcNow.AddHours(7);
            newLichPhongVan.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            newLichPhongVan.TrangThai = true; // Trạng thái hoạt động
            newLichPhongVan = await _unitOfWork.LichPhongVanRepository.AddAsync(newLichPhongVan);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateLichPhongVanResponse>(newLichPhongVan);
        }
    }
}
