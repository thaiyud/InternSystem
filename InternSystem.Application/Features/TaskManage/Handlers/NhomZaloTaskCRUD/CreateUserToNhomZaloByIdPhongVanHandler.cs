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

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class CreateUserToNhomZaloByIdPhongVanHandler : IRequestHandler<CreateUserToNhomZaloByIdCommand, UserNhomZaloTaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public CreateUserToNhomZaloByIdPhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<UserNhomZaloTaskResponse> Handle(CreateUserToNhomZaloByIdCommand request, CancellationToken cancellationToken)
        {
            var ketqua = _config.GetSection("RoleSettings");
            LichPhongVan? exist = await _unitOfWork.LichPhongVanRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete == true)
                throw new ArgumentNullException(nameof(exist), "Lich phong van : null");
            if (exist.KetQua != ketqua["Intern"] && exist.KetQua != ketqua["Leader"] && exist.KetQua == null)
            {
                if (exist.KetQua == _config["Ketqua:Chuadat"])
                    throw new ArgumentException($"Ket qua phong van {request.Id} chua dat");
                throw new ArgumentException($"Ket qua phong van {request.Id} khong ton tai");
            }
            var groupName = await _unitOfWork.NhomZaloRepository.GetNhomZalosByNameAsync(exist.KetQua);
            if (groupName == null || groupName.IsDelete ==true)
                throw new ArgumentException(nameof(groupName), $"Ket qua cua intern {exist.KetQua}  ");
            InternInfo? internInfo = await _unitOfWork.InternInfoRepository.GetByIdAsync(exist.IdNguoiDuocPhongVan);
            if (internInfo == null || internInfo.IsDelete == true)
                throw new ArgumentNullException(nameof(internInfo), "Intern id khong ton tai trong lich phong van");
            var userNhomZalo = new UserNhomZalo
            {
                UserId = internInfo.UserId,
                IdNhomZaloChung = groupName.Id,
                IsMentor = false,
                IsLeader = false,
                CreatedBy = request.CreateBy,
                LastUpdatedBy = request.CreateBy,
                CreatedTime = DateTimeOffset.Now,
                LastUpdatedTime = DateTimeOffset.Now
            };
            await _unitOfWork.UserNhomZaloRepository.AddAsync(userNhomZalo);
            var nhomZaloTask = await _unitOfWork.NhomZaloTaskRepository.GetTaskByNhomZaloIdAsync(groupName.Id);
            foreach ( var item  in nhomZaloTask)
            {
                var userTaskRequest = new CreateUserTaskCommand
                {
                    UserId = internInfo.UserId,
                    TaskId = item.TaskId,
                    CreatedBy = request.CreateBy
                };

                var newUserTask = _mapper.Map<UserTask>(userTaskRequest);
                newUserTask.TrangThai = "Pending";
                newUserTask.LastUpdatedBy = request.CreateBy;
                newUserTask.CreatedTime = DateTimeOffset.Now;
                newUserTask.LastUpdatedTime = DateTimeOffset.Now;
                await _unitOfWork.UserTaskRepository.AddAsync(newUserTask);
            }
            var taskDtos = nhomZaloTask.Select(t => new TaskDto
            {
                TaskId = t.Id,
                TaskName = t.Tasks.MoTa
            }).ToList();

            var User = new UserNhomZaloTaskResponse
            {
                InterviewId = exist.Id,
                KetQua = exist.KetQua,
                GroupName = groupName.TenNhom,
                GroupLink = groupName.LinkNhom,
                CreateBy = request.CreateBy,
                Tasks = taskDtos
            };
            await _unitOfWork.SaveChangeAsync();
            return User;

        }
    }
}
