using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternSystem.Application.Common.Constants;
using InternSystem.Domain.BaseException;
using Microsoft.AspNetCore.Http;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Commands;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Models;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class CreateUserToNhomZaloByIdPhongVanHandler : IRequestHandler<CreateUserToNhomZaloByIdCommand, UserNhomZaloTaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserContextService _userContextService;

        public CreateUserToNhomZaloByIdPhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _userContextService = userContextService;
        }

        public async Task<UserNhomZaloTaskResponse> Handle(CreateUserToNhomZaloByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ketqua = _config.GetSection("RoleSettings");
                LichPhongVan? exist = await _unitOfWork.LichPhongVanRepository.GetByIdAsync(request.Id);
                if (exist == null || exist.IsDelete == true)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy lịch phỏng vấn");

                if (exist.KetQua != ketqua["Intern"] && exist.KetQua != ketqua["Leader"] && exist.KetQua == null)
                {
                    if (exist.KetQua == _config["Ketqua:Chuadat"])
                        throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.INVALID_INPUT, $"Kết quả phỏng vấn {request.Id} chưa đạt");
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.INVALID_INPUT, $"Kết quả phỏng vấn {request.Id} không tồn tại");
                }

                var groupName = await _unitOfWork.NhomZaloRepository.GetNhomZalosByNameAsync(exist.KetQua);
                if (groupName == null || groupName.IsDelete == true)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, $"Kết quả của intern {exist.KetQua} không tìm thấy");

                InternInfo? internInfo = await _unitOfWork.InternInfoRepository.GetByIdAsync(exist.IdNguoiDuocPhongVan);
                if (internInfo == null || internInfo.IsDelete == true)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Intern id không tồn tại trong lịch phỏng vấn");

                var createBy = _userContextService.GetCurrentUserId();
                if (string.IsNullOrEmpty(createBy))
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "CurrentUserId không tìm thấy");

                var userNhomZalo = new UserNhomZalo
                {
                    UserId = internInfo.UserId,
                    IdNhomZaloChung = groupName.Id,
                    IsMentor = false,
                    IsLeader = false,
                    CreatedBy = createBy,
                    LastUpdatedBy = createBy,
                    CreatedTime = DateTimeOffset.Now,
                    LastUpdatedTime = DateTimeOffset.Now
                };
                await _unitOfWork.UserNhomZaloRepository.AddAsync(userNhomZalo);

                var nhomZaloTask = await _unitOfWork.NhomZaloTaskRepository.GetTaskByNhomZaloIdAsync(groupName.Id);
                foreach (var item in nhomZaloTask)
                {
                    var userTaskRequest = new CreateUserTaskCommand
                    {
                        UserId = internInfo.UserId,
                        TaskId = item.TaskId,
                    };

                    var newUserTask = _mapper.Map<UserTask>(userTaskRequest);
                    newUserTask.TrangThai = "Pending";
                    newUserTask.LastUpdatedBy = createBy;
                    newUserTask.CreatedTime = DateTimeOffset.Now;
                    newUserTask.LastUpdatedTime = DateTimeOffset.Now;
                    await _unitOfWork.UserTaskRepository.AddAsync(newUserTask);
                }

                var taskDtos = nhomZaloTask.Select(t => new TaskDto
                {
                    TaskId = t.Id,
                    TaskName = t.Tasks.MoTa
                }).ToList();

                var userNhomZaloTaskResponse = new UserNhomZaloTaskResponse
                {
                    InterviewId = exist.Id,
                    KetQua = exist.KetQua,
                    GroupName = groupName.TenNhom,
                    GroupLink = groupName.LinkNhom,
                    CreateBy = createBy,
                    Tasks = taskDtos
                };

                await _unitOfWork.SaveChangeAsync();
                return userNhomZaloTaskResponse;
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Lỗi tạo user trong Nhom Zalo");
            }
        }
    }
}
