using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Commands;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DuAnManagement.Handlers.CRUD
{
    public class DeleteDuAnHandler : IRequestHandler<DeleteDuAnCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public DeleteDuAnHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<bool> Handle(DeleteDuAnCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.Id)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Dự Án không tồn tại");

                if (existingDA.IsDelete)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Không tìm thấy Dự Án");
                }

                var deleteBy = _userContextService.GetCurrentUserId();
                if (deleteBy.IsNullOrEmpty()) return false;

                existingDA.DeletedBy = deleteBy;
                existingDA.DeletedTime = _timeService.SystemTimeNow;
                existingDA.IsActive = false;
                existingDA.IsDelete = true;

                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi không mong muốn khi lưu");
            }
        }
    }
}
