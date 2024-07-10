using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InternSystem.Application.Common.Constants;
using InternSystem.Domain.BaseException;
using Microsoft.AspNetCore.Http;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Models;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Commands;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class UpdateNhomZaloTaskHandler : IRequestHandler<UpdateNhomZaloTaskCommand, NhomZaloTaskReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserContextService _userContextService;

        public UpdateNhomZaloTaskHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _userContextService = userContextService;
        }

        public async Task<NhomZaloTaskReponse> Handle(UpdateNhomZaloTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exist = await ValidateNhomZaloTaskAsync(request.Id);
                await ValidateAndAssignTaskIdAsync(request, exist);
                await ValidateAndAssignNhomZaloIdAsync(request, exist);

                IEnumerable<NhomZaloTask> existingNhomZaloTasks = await _unitOfWork.NhomZaloTaskRepository.GetAllAsync();
                bool isDuplicateTaskAndGroup = existingNhomZaloTasks.Any(nzt => nzt.TaskId == request.TaskId && nzt.NhomZaloId == request.NhomZaloId && nzt.Id != exist.Id);
                if (isDuplicateTaskAndGroup)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.EXISTED, $"{request.TaskId} đã tồn tại trong nhóm Zalo {request.NhomZaloId}");
                }

                UpdateTrangThai(exist, request.TrangThai);
                if (IsDoneStatus(request.TrangThai))
                {
                    await MarkTaskAsCompletedAsync(request.TaskId);
                }

                var lastUpdatedBy = _userContextService.GetCurrentUserId();
                if (string.IsNullOrEmpty(lastUpdatedBy))
                {
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "CurrentUserId không tìm thấy");
                }

                exist.LastUpdatedBy = lastUpdatedBy;
                exist.LastUpdatedTime = DateTimeOffset.Now;
                await _unitOfWork.SaveChangeAsync();

                return _mapper.Map<NhomZaloTaskReponse>(exist);
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Lỗi cập nhật task trong nhóm Zalo");
            }
        }

        private async Task<NhomZaloTask> ValidateNhomZaloTaskAsync(int id)
        {
            var exist = await _unitOfWork.NhomZaloTaskRepository.GetByIdAsync(id);
            if (exist == null || exist.IsDelete)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, $"Task {id} không tồn tại");
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
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, $"Task id {request.TaskId} không tồn tại");
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
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, $"Nhóm zalo id {request.NhomZaloId} không tồn tại");
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
