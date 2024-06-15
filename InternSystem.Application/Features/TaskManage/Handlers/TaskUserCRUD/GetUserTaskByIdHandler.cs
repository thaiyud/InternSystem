using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Application.Features.TaskManage.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskUserCRUD
{
    public class GetUserTaskByIdHandler : IRequestHandler<GetUserTaskByIdQuery, UserTaskReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserTaskByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserTaskReponse> Handle(GetUserTaskByIdQuery request, CancellationToken cancellationToken)
        {
            UserTask? exist = await _unitOfWork.UserTaskRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete == true)
                throw new ArgumentNullException(
                    nameof(request.Id),
                    $"UserTask {request.Id} not found");

            return _mapper.Map<UserTaskReponse>(exist);
        }
    }
}
