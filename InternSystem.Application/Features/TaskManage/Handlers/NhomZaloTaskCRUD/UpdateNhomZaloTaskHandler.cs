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

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class UpdateNhomZaloTaskHandler : IRequestHandler<UpdateNhomZaloTaskCommand, NhomZaloTaskReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UpdateNhomZaloTaskHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<NhomZaloTaskReponse> Handle(UpdateNhomZaloTaskCommand request, CancellationToken cancellationToken)
        {
            var exist = await ValidateNhomZaloTaskAsync(request.Id);
            await ValidateAndAssignTaskIdAsync(request, exist);
            await ValidateAndAssignNhomZaloIdAsync(request, exist);

            if (exist.TaskId == request.TaskId && exist.NhomZaloId == request.NhomZaloId)
            {
                throw new ArgumentNullException(nameof(request), $"{request.TaskId} is already exist by {request.NhomZaloId}");
            }

            UpdateTrangThai(exist, request.TrangThai);
            if (IsDoneStatus(request.TrangThai))
            {
                await MarkTaskAsCompletedAsync(request.TaskId);
            }

            exist.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<NhomZaloTaskReponse>(exist);
        }

        private async Task<NhomZaloTask> ValidateNhomZaloTaskAsync(int id)
        {
            var exist = await _unitOfWork.NhomZaloTaskRepository.GetByIdAsync(id);
            if (exist == null || exist.IsDelete)
            {
                throw new ArgumentNullException(nameof(id), $"Task {id} does not exist");
            }

            return exist;
        }

        private async Task ValidateAndAssignTaskIdAsync(UpdateNhomZaloTaskCommand request, NhomZaloTask exist)
        {
            if (request.TaskId.HasValue)
            {
                var task = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId.Value);
                if (task == null || task.IsDelete)
                {
                    throw new ArgumentNullException(nameof(request.TaskId), $"Task id {request.TaskId} does not exist");
                }

                exist.TaskId = request.TaskId.Value;
            }
        }

        private async Task ValidateAndAssignNhomZaloIdAsync(UpdateNhomZaloTaskCommand request, NhomZaloTask exist)
        {
            if (request.NhomZaloId.HasValue)
            {
                var nhomZalo = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.NhomZaloId.Value);
                if (nhomZalo == null || nhomZalo.IsDelete)
                {
                    throw new ArgumentNullException(nameof(request.NhomZaloId), $"Nhom zalo id {request.NhomZaloId} does not exist");
                }

                exist.NhomZaloId = request.NhomZaloId.Value;
            }
        }

        private void UpdateTrangThai(NhomZaloTask exist, string trangThai)
        {
            if (!string.IsNullOrWhiteSpace(trangThai) && IsValidTrangThai(trangThai))
            {
                exist.TrangThai = trangThai;
            }
        }

        private bool IsValidTrangThai(string trangThai)
        {
            var validTrangThai = new[]
            {
                _config["TrangThai:Done"],
                _config["TrangThai:Processing"],
                _config["TrangThai:Late"],
                _config["TrangThai:Pending"]
            };

            return validTrangThai.Any(status => trangThai.Contains(status, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsDoneStatus(string trangThai)
        {
            return trangThai.Contains(_config["TrangThai:Done"], StringComparison.OrdinalIgnoreCase);
        }

        private async Task MarkTaskAsCompletedAsync(int? taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.HoanThanh = true;
            }
        }
    }

    
}
