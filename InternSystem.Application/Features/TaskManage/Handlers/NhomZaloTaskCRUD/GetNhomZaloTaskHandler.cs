using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Application.Features.TaskManage.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    internal class GetNhomZaloTaskHandler : IRequestHandler<GetNhomZaloTaskByQuery, IEnumerable<NhomZaloTaskReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetNhomZaloTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NhomZaloTaskReponse>> Handle(GetNhomZaloTaskByQuery request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.NhomZaloTaskRepository.GetAllAsync();
            Console.WriteLine(exist);
            return _mapper.Map<IEnumerable<NhomZaloTaskReponse>>(exist);
        }
    
    }
}
