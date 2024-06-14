using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Application.Features.TaskManage.Commands.Delete;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskCRUD
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            Tasks? exist = await _unitOfWork.TaskRepository.GetByIdAsync(request.Id);
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
