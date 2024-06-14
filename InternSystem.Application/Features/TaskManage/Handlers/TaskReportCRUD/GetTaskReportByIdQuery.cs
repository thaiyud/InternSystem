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

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskReportCRUD
{
    public class GetTaskReportByIdHandler : IRequestHandler<GetTaskReportByIdQuery, TaskReportResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTaskReportByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaskReportResponse> Handle(GetTaskReportByIdQuery request, CancellationToken cancellationToken)
        {
            ReportTask? exist = await _unitOfWork.ReportTaskRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete == true)
                throw new ArgumentNullException(
                    nameof(request.Id),
                    $"Task Report {request.Id} not found");

            return _mapper.Map<TaskReportResponse>(exist);
        }
    }
}
