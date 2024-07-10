using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TasksAndReports.TaskManagement.Models;
using InternSystem.Application.Features.TasksAndReports.TaskManagement.Queries;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.TasksAndReports.TaskManagement.Handlers
{
    public class GetTaskByMoTaHandler : IRequestHandler<GetTaskByMoTaQuery, IEnumerable<TaskResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskByMoTaHandler(IMapper mapper, IUnitOfWork taskRepository)
        {
            _mapper = mapper;
            _unitOfWork = taskRepository;
        }
        public async Task<IEnumerable<TaskResponse>> Handle(GetTaskByMoTaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Tasks> tasks = await _unitOfWork.TaskRepository.GetTasksByMoTaAsync(request.mota);
                if (tasks.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy task");
                }

                return _mapper.Map<IEnumerable<TaskResponse>>(tasks);
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi không mong muốn khi lấy dữ liệu.");
            }

        }
    }
}
