using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Application.Features.TasksAndReports.TaskManagement.Commands;
using InternSystem.Application.Features.TasksAndReports.TaskManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskCRUD
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;


        public UpdateTaskHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<TaskResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            Tasks? exist = await _unitOfWork.TaskRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete == true || exist.HoanThanh == true)
                throw new ArgumentNullException(
                   nameof(request.Id),
                   $"Task {request.Id} is not exist");

            if (request.DuAnId > 0)
            {
                DuAn? duAn = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId);
                if (duAn == null || duAn.IsDelete == true)                
                    throw new ArgumentNullException(
                  nameof(request.DuAnId),
                  $"Du an id {request.DuAnId} is not exist");
                if (request.HanHoanThanh > duAn.ThoiGianKetThuc
              || request.NgayGiao < duAn.ThoiGianBatDau)
                    throw new ArgumentNullException(
                      nameof(request),
                      $"set time incorrect in the range time Du An: {duAn.Id}");
                exist.DuAnId = (int)request.DuAnId;

            }
            IEnumerable<Tasks>? exist2 = await _unitOfWork.TaskRepository.GetAllAsync();
            List<Tasks>? list = exist2.ToList();
            foreach (var item in list)
            {
                if (item.MoTa == request.MoTa && item.DuAnId == request.DuAnId)
                    throw new ArgumentNullException(
                     nameof(request), $"{request.MoTa} is already exist by {request.DuAnId}");

            }

            // Kiểm tra thời gian hoàn thành và ngày giao task 
            if (request.HanHoanThanh < request.NgayGiao
                || exist.NgayGiao > request.HanHoanThanh
                || request.NgayGiao > exist.HanHoanThanh)
                throw new ArgumentNullException(
                  nameof(request),
                  $"Han hoan thanh must greater than Ngay Giao");

            if (!string.IsNullOrWhiteSpace(request.MoTa))
                exist.MoTa = request.MoTa;
            if (!string.IsNullOrWhiteSpace(request.NoiDung))
                exist.NoiDung = request.NoiDung;
            if (request.NgayGiao.HasValue)
                exist.NgayGiao = request.NgayGiao.Value;
            if (request.HanHoanThanh.HasValue)
                exist.HanHoanThanh = request.HanHoanThanh.Value;
            if (request.HoanThanh.HasValue)
                exist.HoanThanh = request.HoanThanh.Value;

            var lastUpdatedBy = _userContextService.GetCurrentUserId();
            if (lastUpdatedBy.IsNullOrEmpty()) throw new ArgumentNullException("CurrentUserId not found");

            exist.LastUpdatedBy = lastUpdatedBy;
            exist.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<TaskResponse>(exist);
        }
    }
}
