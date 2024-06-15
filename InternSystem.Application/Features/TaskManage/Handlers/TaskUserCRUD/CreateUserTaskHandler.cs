

using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace InternSystem.Application.Features.TaskManage.Han
{
    public class CreateUserTaskHandler : IRequestHandler<CreateUserTaskCommand, UserTaskReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public CreateUserTaskHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<UserTaskReponse> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
        {
            AspNetUser? exist = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (exist == null
                || exist.IsDelete == true)
                throw new ArgumentNullException(
                    nameof(request.UserId), "User not found");
            Tasks? exist1 = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
            if (exist1 == null
                || exist1.IsDelete == true || exist1.HoanThanh ==true)
                throw new ArgumentNullException(
                    nameof(request.TaskId), "Task not found");
            IEnumerable<UserTask>? exist2 = await _unitOfWork.UserTaskRepository.GetAllAsync();
            List<UserTask>? list = exist2.ToList();
            foreach (var item in list)
            {
                if (item.TaskId == request.TaskId && item.UserId == request.UserId)
                    throw new ArgumentNullException(
                     nameof(request), $"{request.TaskId} is already exist by {request.UserId}");

            }
            var newTask = _mapper.Map<UserTask>(request);
            newTask.TrangThai = _config["TrangThai:Pending"];
            newTask.CreatedTime = DateTimeOffset.Now;
            newTask.LastUpdatedTime = DateTimeOffset.Now;
            newTask.LastUpdatedBy = request.CreatedBy;
            newTask = await _unitOfWork.UserTaskRepository.AddAsync(newTask);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<UserTaskReponse>(newTask);


        }
    } 
}
