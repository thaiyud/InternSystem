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

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskUserCRUD
{
    public class UpdateUserTaskHandler : IRequestHandler<UpdateUserTaskCommand, UserTaskReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IConfiguration _config;

        public UpdateUserTaskHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }


        public async Task<UserTaskReponse> Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.UserTaskRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete)
            {
                throw new ArgumentNullException(nameof(request), $"User Task {request.Id} does not exist");
            }

            await ValidateAndAssignUserTaskAsync(request, exist);

            if (!string.IsNullOrWhiteSpace(request.TrangThai))
            {
                UpdateTrangThai(request, exist);
            }

            exist.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UserTaskReponse>(exist);
        }

        private async Task ValidateAndAssignUserTaskAsync(UpdateUserTaskCommand request, UserTask exist)
        {
            if (request.TaskId != null)
            {
                // Check if the combination of TaskId and UserId already exists
                var existingUserTasks = await _unitOfWork.UserTaskRepository.GetAllAsync();
                if (existingUserTasks.Any(ut => ut.TaskId == request.TaskId && ut.UserId == request.UserId && ut.Id != exist.Id))
                {
                    throw new ArgumentException($"User {request.UserId} is already assigned to Task {request.TaskId}");
                }

                // Validate and assign TaskId
                var tasks = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
                if (tasks == null || tasks.IsDelete)
                {
                    throw new ArgumentNullException(nameof(request), $"Task {request.TaskId} does not exist");
                }

                exist.TaskId = (int)request.TaskId;
            }

            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                // Validate and assign UserId
                var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
                if (user == null || user.IsDelete)
                {
                    throw new ArgumentNullException(nameof(request), $"User {request.UserId} does not exist");
                }

                exist.UserId = request.UserId;
            }
        }

        private void UpdateTrangThai(UpdateUserTaskCommand request, UserTask exist)
        {
            var trangThaiLower = request.TrangThai.ToLower();
            var validTrangThai = new[] {
                _config["TrangThai:Done"]?.ToLower(),
                _config["TrangThai:Processing"]?.ToLower(),
                _config["TrangThai:Late"]?.ToLower(),
                _config["TrangThai:Pending"]?.ToLower()
            };

            if (Array.Exists(validTrangThai, status => trangThaiLower.Contains(status)))
            {
                exist.TrangThai = request.TrangThai;

                if (trangThaiLower.Contains(_config["TrangThai:Done"]!.ToLower()))
                {
                    MarkTaskAsCompleteAsync(request.TaskId);
                }
            }
        }

        private async Task MarkTaskAsCompleteAsync(int? taskId)
        {
            if (taskId.HasValue)
            {
                var tasks = await _unitOfWork.TaskRepository.GetByIdAsync(taskId.Value);
                if (tasks != null)
                {
                    tasks.HoanThanh = true;
                }
            }
        }
    }
}