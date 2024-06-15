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
    public class InternToLeaderHandler : IRequestHandler<PromoteMemberToLeaderCommand,ExampleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InternToLeaderHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

       
       public async Task<ExampleResponse> Handle(PromoteMemberToLeaderCommand request, CancellationToken cancellationToken)
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

            var currentUserId = user.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new Exception("User ID claim is not found");
            }

            var group = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.NhomZaloId);
            if (group == null)
            {
                throw new ArgumentException("Nhom zalo is not valid.");
            }

            var mentor = await _unitOfWork.UserNhomZaloRepository.GetByUserIdAndNhomZaloIdAsync(currentUserId, group.Id);
            if (mentor == null || !mentor.IsMentor || mentor.IsDelete == true)
            {
                throw new ArgumentException("User ID is not mentor.");
            }

            var member = await _unitOfWork.UserNhomZaloRepository.GetByUserIdAndNhomZaloIdAsync(request.MemberId, group.Id);
            if (member == null || member.IsDelete == true)
            {
                throw new ArgumentException("Member is not valid.");
            }

            var nhomzalotask = await _unitOfWork.NhomZaloTaskRepository.GetTaskByNhomZaloIdAsync(group.Id);
            foreach (var item in nhomzalotask)
            {
                var tasks = await _unitOfWork.TaskRepository.GetByIdAsync(item.TaskId);
                if (tasks == null || tasks.IsDelete == true || tasks.DuAnId != request.DuanId)
                {
                    throw new ArgumentException("Du an is not valid for nhom zalo.");
                }
            }

            member.IsLeader = true;
            member.LastUpdatedBy = currentUserId;
            member.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.UserNhomZaloRepository.UpdateUserNhomZaloAsync(member);

            var projectToUpdate = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuanId);
            if (projectToUpdate == null)
            {
                throw new ArgumentException("Project not found");
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
    }
}
