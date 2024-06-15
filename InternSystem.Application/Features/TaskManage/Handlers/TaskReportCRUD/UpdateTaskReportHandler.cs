using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Commands.Update;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskReportCRUD
{
    public class UpdateTaskReportHandler : IRequestHandler<UpdateTaskReportCommand, TaskReportResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UpdateTaskReportHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<TaskReportResponse> Handle(UpdateTaskReportCommand request, CancellationToken cancellationToken)
        {
            ReportTask? exist = await _unitOfWork.ReportTaskRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete == true)
                throw new ArgumentNullException(
                   nameof(request),
                   $"Task Report {request.Id} is not exist");

            if (request.TaskId !=null)
            {
                Tasks? tasks = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
                if (tasks == null || tasks.IsDelete == true)
                    throw new ArgumentNullException(
                  nameof(request),
                  $"Task {request.TaskId} is not exist");
            }
            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                AspNetUser? asp = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
                if (asp == null || asp.IsDelete == true)
                    throw new ArgumentNullException(
                  nameof(request),
                  $"Task {request.UserId} is not exist");
            }
            if (!string.IsNullOrWhiteSpace(request.TrangThai) && (
                request.TrangThai.ToLower().Contains(_config["TrangThai:Done"]!) ||
                request.TrangThai.ToLower().Contains(_config["TrangThai:Processing"]!) ||
                request.TrangThai.ToLower().Contains(_config["TrangThai:Late"]!) ||
                request.TrangThai.ToLower().Contains(_config["TrangThai:Pending"]!)))
                exist.TrangThai = request.TrangThai;
            if (!string.IsNullOrWhiteSpace(request.MoTa))
                exist.MoTa = request.MoTa;
            if (!string.IsNullOrWhiteSpace(request.NoiDungBaoCao))
            { exist.NoiDungBaoCao = request.NoiDungBaoCao; }

            exist.NgayBaoCao = DateTime.Now;
            exist.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<TaskReportResponse>(exist);
        }
    }
}
