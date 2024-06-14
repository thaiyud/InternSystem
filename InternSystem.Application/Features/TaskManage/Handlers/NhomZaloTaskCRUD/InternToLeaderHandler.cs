using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Application.Features.TaskManage.Commands;
using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class InternToLeaderHandler : IRequestHandler<PromoteMemberToLeaderCommand,LeaderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InternToLeaderHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       
       public async Task<LeaderResponse> Handle(PromoteMemberToLeaderCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext is null");
            }

            var user = httpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var currentUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new Exception("User ID claim is not found");
            }
            var mentor = await _unitOfWork.UserNhomZaloRepository.GetByIdAsync(currentUserId);
            if (mentor == null || !mentor.IsMentor || mentor.IsDelete == true)
            {
                throw new ArgumentException("Mentor is not valid.");
            }
            var group = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.NhomZaloId);
            if (group == null)
            {
                throw new ArgumentException("Nhom zalo is not valid.");
            }
            var member = await _unitOfWork.UserNhomZaloRepository.GetByUserIdAndNhomZaloIdAsync(request.MemberId, group.Id);
            if (member == null || member.IsDelete == true)
            {
                throw new ArgumentException("Member is not valid.");
            }
            var project = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuanId);
            if (project == null || project.IsDelete == true)
            {
                throw new ArgumentException("Du an is not valid.");
            }
            member.IsLeader = true;
            member.LastUpdatedBy = currentUserId;
            member.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.UserNhomZaloRepository.UpdateUserNhomZaloAsync(member);

            project.LeaderId = request.MemberId;
            project.LastUpdatedBy = currentUserId;
            project.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.DuAnRepository.UpdateDuAnAsync(project);
            // Save changes
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<LeaderResponse>(request);
        }
    }
}
