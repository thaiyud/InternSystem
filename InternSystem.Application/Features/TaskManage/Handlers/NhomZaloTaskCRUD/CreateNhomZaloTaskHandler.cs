using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternSystem.Application.Common.Constants;
using InternSystem.Domain.BaseException;
using Microsoft.AspNetCore.Http;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Commands;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Models;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Commands;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class CreateNhomZaloTaskHandler : IRequestHandler<CreateNhomZaloTaskCommand, NhomZaloTaskReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserContextService _userContextService;

        public CreateNhomZaloTaskHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _userContextService = userContextService;
        }

        public async Task<NhomZaloTaskReponse> Handle(CreateNhomZaloTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                NhomZalo? exist = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.NhomZaloId);
                if (exist == null || exist.IsDelete == true)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy nhóm Zalo");

                Tasks? exist1 = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
                if (exist1 == null || exist1.IsDelete == true || exist1.HoanThanh == true)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy Task");

                IEnumerable<NhomZaloTask>? exist2 = await _unitOfWork.NhomZaloTaskRepository.GetAllAsync();
                List<NhomZaloTask>? list = exist2.ToList();
                foreach (var item in list)
                {
                    if (item.TaskId == request.TaskId && item.NhomZaloId == request.NhomZaloId)
                        throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.DUPLICATE, $"{request.TaskId} đã tồn tại trong nhóm Zalo {request.NhomZaloId}");
                }

                IEnumerable<UserNhomZalo> userNhomZalos = await _unitOfWork.UserNhomZaloRepository.GetByNhomZaloIdAsync(request.NhomZaloId);

                var createdBy = _userContextService.GetCurrentUserId();
                if (string.IsNullOrEmpty(createdBy))
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "CurrentUserId không tìm thấy");

                // Gán nhóm zalo vào task
                var newNhomZaloTask = _mapper.Map<NhomZaloTask>(request);
                newNhomZaloTask.TrangThai = _config["TrangThai:Pending"];
                newNhomZaloTask.CreatedTime = DateTimeOffset.Now;
                newNhomZaloTask.LastUpdatedTime = DateTimeOffset.Now;
                newNhomZaloTask.LastUpdatedBy = createdBy;
                newNhomZaloTask = await _unitOfWork.NhomZaloTaskRepository.AddAsync(newNhomZaloTask);

                // Thêm user từ nhóm zalo *CHUNG* vào usertask
                foreach (var userId in userNhomZalos)
                {
                    var userTaskRequest = new CreateUserTaskCommand
                    {
                        UserId = userId.UserId,
                        TaskId = request.TaskId,
                      
                    };

                    var newUserTask = _mapper.Map<UserTask>(userTaskRequest);
                    newUserTask.CreatedTime = DateTimeOffset.Now;
                    newUserTask.LastUpdatedTime = DateTimeOffset.Now;
                    newUserTask.LastUpdatedBy = createdBy;
                    await _unitOfWork.UserTaskRepository.AddAsync(newUserTask);
                }

                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<NhomZaloTaskReponse>(newNhomZaloTask);
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Lỗi tạo task trong nhóm Zalo");
            }
        }
    }
}
