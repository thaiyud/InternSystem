using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Queries;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Application.Features.TaskManage.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskCRUD
{
    public class GetTaskHandler : IRequestHandler<GetTaskQuery, IEnumerable<TaskResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TaskResponse>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.TaskRepository.GetAllASync();
            Console.WriteLine(exist);
            return _mapper.Map<IEnumerable<TaskResponse>>(exist);
        }
    }
}
