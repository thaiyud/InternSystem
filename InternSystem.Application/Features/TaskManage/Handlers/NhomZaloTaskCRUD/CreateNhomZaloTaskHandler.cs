using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class CreateNhomZaloTaskHandler :  IRequestHandler<CreateNhomZaloTaskCommand, NhomZaloTaskReponse>
    { private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public CreateNhomZaloTaskHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<NhomZaloTaskReponse> Handle(CreateNhomZaloTaskCommand request, CancellationToken cancellationToken)
        {
            NhomZalo? exist = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.NhomZaloId);
            if (exist == null
               || exist.IsDelete == true)
                throw new ArgumentNullException(
                    nameof(request), "Nhom zalo not found");
            Tasks? exist1 = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
            if (exist1 == null
                || exist1.IsDelete == true
                || exist1.HoanThanh == true)
                throw new ArgumentNullException(
                    nameof(request), "Task not found");
            IEnumerable<NhomZaloTask>? exist2 = await _unitOfWork.NhomZaloTaskRepository.GetAllAsync();
               List<NhomZaloTask>? list = exist2.ToList();
            foreach(var item in list)
            {
                if(item.TaskId == request.TaskId && item.NhomZaloId == request.NhomZaloId)
                    throw new ArgumentNullException(
                     nameof(request), $"{request.TaskId} is already exist by {request.NhomZaloId}");

            }
            IEnumerable<UserNhomZalo> userNhomZalos = await _unitOfWork.UserNhomZaloRepository.GetByNhomZaloIdAsync(request.NhomZaloId);
            // gan nhom zalo vao task
            var newNhomZaloTask = _mapper.Map<NhomZaloTask>(request);
            newNhomZaloTask.TrangThai = _config["TrangThai:Pending"];
            newNhomZaloTask.CreatedTime = DateTimeOffset.Now;
            newNhomZaloTask.LastUpdatedTime = DateTimeOffset.Now;
            newNhomZaloTask.LastUpdatedBy = request.CreatedBy;
            newNhomZaloTask = await _unitOfWork.NhomZaloTaskRepository.AddAsync(newNhomZaloTask);
            // them user tu nhom zalo *CHUNG* vao usertask
            foreach (var userId in userNhomZalos)
            {
                var userTaskRequest = new CreateUserTaskCommand
                {
                    UserId = userId.UserId,
                    TaskId = request.TaskId,
                    CreatedBy = request.CreatedBy
                };

                var newUserTask = _mapper.Map<UserTask>(userTaskRequest);
                newUserTask.CreatedTime = DateTimeOffset.Now;
                newUserTask.LastUpdatedTime = DateTimeOffset.Now;
                newUserTask.LastUpdatedBy = request.CreatedBy;
                await _unitOfWork.UserTaskRepository.AddAsync(newUserTask);
            }
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<NhomZaloTaskReponse>(newNhomZaloTask);
        }
    }
}
