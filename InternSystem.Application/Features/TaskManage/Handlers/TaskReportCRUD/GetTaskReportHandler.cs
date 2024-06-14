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

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskReportCRUD
{
    public class GetTaskReportHandler : IRequestHandler<GetTaskReportQuery, IEnumerable<TaskReportResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTaskReportHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TaskReportResponse>> Handle(GetTaskReportQuery request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.ReportTaskRepository.GetAllASync();
            Console.WriteLine(exist);
            return _mapper.Map<IEnumerable<TaskReportResponse>>(exist);
        }
    }
}
