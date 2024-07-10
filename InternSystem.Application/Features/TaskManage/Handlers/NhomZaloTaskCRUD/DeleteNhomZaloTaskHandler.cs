using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSystem.Application.Common.Constants;
using InternSystem.Domain.BaseException;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Commands;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class DeleteNhomZaloTaskHandler : IRequestHandler<DeleteNhomZaloTaskCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;

        public DeleteNhomZaloTaskHandler(IUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
        }

        public async Task<bool> Handle(DeleteNhomZaloTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                NhomZaloTask? exist = await _unitOfWork.NhomZaloTaskRepository.GetByIdAsync(request.Id);
                if (exist == null || exist.IsDelete == true)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, $"Task với ID {request.Id} không tồn tại hoặc đã bị xoá");

                var deleteBy = _userContextService.GetCurrentUserId();
                if (string.IsNullOrEmpty(deleteBy))
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "CurrentUserId không tìm thấy");

                exist.DeletedBy = deleteBy;
                exist.DeletedTime = DateTimeOffset.Now;
                exist.IsActive = false;
                exist.IsDelete = true;
                await _unitOfWork.SaveChangeAsync();

                return true;
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Lỗi xoá task trong nhóm Zalo");
            }
        }
    }
}
