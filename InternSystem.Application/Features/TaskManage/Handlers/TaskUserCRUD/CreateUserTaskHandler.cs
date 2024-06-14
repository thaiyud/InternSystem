

using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.TaskManage.Han
{
    public class CreateUserTaskHandler : IRequestHandler<CreateUserTaskCommand, UserTaskReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserTaskReponse> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
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
            var newTask = _mapper.Map<UserTask>(request);
            newTask.CreatedTime = DateTimeOffset.Now;
            newTask.LastUpdatedTime = DateTimeOffset.Now;
            newTask.LastUpdatedBy = request.CreatedBy;
            newTask = await _unitOfWork.UserTaskRepository.AddAsync(newTask);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<UserTaskReponse>(newTask);


        }
    } 
}
