using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Commands.Create;
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
    public class CreateTaskReportHandler : IRequestHandler<CreateTaskReportCommand, TaskReportResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IConfiguration _config;

        public CreateTaskReportHandler(IUnitOfWork unitOfWork, IMapper mapper,IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<TaskReportResponse> Handle(CreateTaskReportCommand request, CancellationToken cancellationToken)

        {
           
            AspNetUser? exist = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (exist == null
                || exist.IsDelete == true)
                throw new ArgumentNullException(
                    nameof(request), "User not found");
            Tasks? exist1 = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
            if (exist1 == null
                || exist1.IsDelete == true)
                throw new ArgumentNullException(
                    nameof(request), "Task not found");
            ReportTask newTask = _mapper.Map<ReportTask>(request);
           
            newTask.TrangThai = _config["TrangThai:Pending"]!;
            newTask.CreatedTime = DateTimeOffset.Now;
            newTask.LastUpdatedTime = DateTimeOffset.Now;
            newTask.LastUpdatedBy = request.CreatedBy;
            await _unitOfWork.ReportTaskRepository.AddAsync(newTask);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<TaskReportResponse>(newTask);
        }

     
    }
}
