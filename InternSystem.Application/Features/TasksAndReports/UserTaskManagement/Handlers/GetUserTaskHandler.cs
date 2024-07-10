using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Models;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Queries;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Handlers
{
    public class GetUserTaskHandler : IRequestHandler<GetUserTaskQuery, IEnumerable<UserTaskReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserTaskReponse>> Handle(GetUserTaskQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<UserTask> userTasks = await _unitOfWork.UserTaskRepository.GetUserTasksAsync();
                return _mapper.Map<IEnumerable<UserTaskReponse>>(userTasks);
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
