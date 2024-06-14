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
            var exist = await _unitOfWork.UserTaskRepository.GetAllASync();
            Console.WriteLine(exist);
            return _mapper.Map<IEnumerable<UserTaskReponse>>(exist);
        }
    }
}
