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
       
            DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId);
            if (existingDA == null 
                || existingDA.IsDelete == true)
                throw new ArgumentNullException(
                    nameof(request), "DuAn not found");
            IEnumerable<Tasks>? exist2 = await _unitOfWork.TaskRepository.GetAllAsync();
            List<Tasks>? list = exist2.ToList();
            foreach (var item in list)
            {
                if (item.MoTa == request.MoTa && item.DuAnId == request.DuAnId)
                    throw new ArgumentNullException(
                     nameof(request), $"{request.MoTa} is already exist by {request.DuAnId}");

            }
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
