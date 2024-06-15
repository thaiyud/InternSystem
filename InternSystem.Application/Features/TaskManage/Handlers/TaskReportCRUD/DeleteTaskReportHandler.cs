    using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TaskManage.Commands.Delete;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskReportCRUD
{
    public class DeleteTaskReportHandler : IRequestHandler<DeleteTaskReportCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskReportHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTaskReportCommand request, CancellationToken cancellationToken)
        {
            ReportTask? exist = await _unitOfWork.ReportTaskRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete == true)
                return false;
            exist.DeletedBy = request.DeletedBy;
            exist.DeletedTime = DateTimeOffset.Now;
            exist.IsActive = false;
            exist.IsDelete = true;
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
