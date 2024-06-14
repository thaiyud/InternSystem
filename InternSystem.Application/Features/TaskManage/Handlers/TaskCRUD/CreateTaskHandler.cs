using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskCRUD
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Tasks> exist = await _unitOfWork.TaskRepository.GetTasksByNameAsync(request.MoTa);
            if (exist.Any() 
                && (request.DuAnId == exist.FirstOrDefault().DuAnId))
                throw new ArgumentNullException(
                    nameof(request.DuAnId),
                    $"Task {request.MoTa} is already exist in project {request.DuAnId}");
            DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId);
            if (existingDA == null 
                || existingDA.IsDelete == true)
                throw new ArgumentNullException(
                    nameof(request), "DuAn not found");
            Tasks newTask = _mapper.Map<Tasks>(request);
                  newTask.CreatedTime = DateTimeOffset.Now;
                  newTask.LastUpdatedTime = DateTimeOffset.Now;
                  newTask.LastUpdatedBy = request.CreatedBy;
                  newTask.HoanThanh = false;
                  newTask = await _unitOfWork.TaskRepository.AddAsync(newTask);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<TaskResponse>(newTask);
        }
    }
}
