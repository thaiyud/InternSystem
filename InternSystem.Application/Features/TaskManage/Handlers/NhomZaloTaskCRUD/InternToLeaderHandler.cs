using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InternSystem.Application.Common.Constants;
using InternSystem.Domain.BaseException;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Commands;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Models;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class InternToLeaderHandler : IRequestHandler<PromoteMemberToLeaderCommand, ExampleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextService _userContextService;

        public InternToLeaderHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
            IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userContextService = userContextService;
        }

        public async Task<ExampleResponse> Handle(PromoteMemberToLeaderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "HttpContext rỗng");
                }

                var user = httpContext.User;
                if (user == null || !user.Identity.IsAuthenticated)
                {
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "Người dùng chưa xác thực");
                }

                string currentUserId = _userContextService.GetCurrentUserId();
                if (string.IsNullOrEmpty(currentUserId))
                {
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "Không tìm thấy CurrentUserId");
                }

                var group = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.NhomZaloId);
                if (group == null)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Nhóm Zalo không hợp lệ");
                }

                var mentor = await _unitOfWork.UserNhomZaloRepository.GetByUserIdAndNhomZaloIdAsync(currentUserId, group.Id);
                if (mentor == null || !mentor.IsMentor || mentor.IsDelete == true)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.INVALID_INPUT, "User ID không là người hướng dẫn");
                }

                var member = await _unitOfWork.UserNhomZaloRepository.GetByUserIdAndNhomZaloIdAsync(request.MemberId, group.Id);
                if (member == null || member.IsDelete == true)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Thành viên không hợp lệ");
                }

                var nhomzalotask = await _unitOfWork.NhomZaloTaskRepository.GetTaskByNhomZaloIdAsync(group.Id);
                foreach (var item in nhomzalotask)
                {
                    var tasks = await _unitOfWork.TaskRepository.GetByIdAsync(item.TaskId);
                    if (tasks == null || tasks.DuAnId != request.DuanId)
                    {
                        throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.INVALID_INPUT, "Dự án không hợp lệ cho nhóm Zalo");
                    }
                }

                member.IsLeader = true;
                member.LastUpdatedBy = currentUserId;
                member.LastUpdatedTime = DateTimeOffset.Now;
                await _unitOfWork.UserNhomZaloRepository.UpdateUserNhomZaloAsync(member);

                var projectToUpdate = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuanId);
                if (projectToUpdate == null || projectToUpdate.IsDelete == true || projectToUpdate.ThoiGianKetThuc < DateTimeOffset.Now)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Dự án không được tìm thấy");
                }
                projectToUpdate.LeaderId = request.MemberId;
                projectToUpdate.LastUpdatedBy = currentUserId;
                projectToUpdate.LastUpdatedTime = DateTimeOffset.Now;
                await _unitOfWork.DuAnRepository.UpdateDuAnAsync(projectToUpdate);

                await _unitOfWork.SaveChangeAsync();
                var response = new ExampleResponse
                {
                    NhomZaloId = group.Id,
                    Leader = member.UserId,
                    Mentor = mentor.UserId,
                    Task = new TaskDetails
                    {
                        DuanId = projectToUpdate.Id,
                        Leader = request.MemberId
                    }
                };
                return response;
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Lỗi tạo task trong nhóm Zalo");
            }
        }
    }
}
