using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Commands.Update;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Domain.Entities;
using MediatR;
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

        public UpdateUserTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserTaskReponse> Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
        {
            UserTask? exist = await _unitOfWork.UserTaskRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete == true)
                throw new ArgumentNullException(
                   nameof(request),
                   $"User Task {request.Id} is not exist");

            if (request.TaskId != exist.TaskId)
            {
                Tasks? tasks = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
                if (tasks == null || tasks.IsDelete == true)
                    throw new ArgumentNullException(
                  nameof(request),
                  $"Task {request.TaskId} is not exist");
                exist.TaskId = (int)request.TaskId;
            }
            if (request.UserId != exist.UserId)
            {
                AspNetUser? asp = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
                if (asp == null || asp.IsDelete == true)
                    throw new ArgumentNullException(
                  nameof(request),
                  $"Task {request.UserId} is not exist");
                exist.UserId = request.UserId;
            }

            exist.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UserTaskReponse>(exist);
        }
    }
}
