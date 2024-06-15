using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class AddUserToNhomZaloCommandHandler : IRequestHandler<AddUserToNhomZaloCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AddUserToNhomZaloCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
         
        }
        public async Task<bool> Handle(AddUserToNhomZaloCommand request, CancellationToken cancellationToken)
        {

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext is null");
            }

            var userCurrent = httpContext.User;
            if (userCurrent == null || !userCurrent.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var currentUserId = userCurrent.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new Exception("User ID claim is not found");
            }

            // Retrieve user and nhomZalo from repositories
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            var nhomZalo = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.NhomZaloId);
    

            if (user == null || nhomZalo == null)
                return false;


            // Check if user is already in the group
            var existingUserNhomZalo = await _unitOfWork.UserNhomZaloRepository.GetByUserIdAndNhomZaloIdAsync(request.UserId, request.NhomZaloId);
            if (existingUserNhomZalo != null)
            {
                return false; // User is already in the group
            }


            // Create UserNhomZalo entity
            var userNhomZalo = new UserNhomZalo
            {
                UserId = request.UserId,
                IsMentor = request.IsMentor,
                IsLeader = request.IsLeader,
                IdNhomZaloChung = (int?)(nhomZalo.IsNhomChung ? request.NhomZaloId : (int?)null),
                IdNhomZaloRieng = (int?)(nhomZalo.IsNhomChung ? (int?)null : request.NhomZaloId),
                CreatedBy = currentUserId,
                LastUpdatedBy = currentUserId,
                CreatedTime = DateTimeOffset.Now,
                LastUpdatedTime = DateTimeOffset.Now
            };

            // Add the entity to the repository
            _unitOfWork.UserNhomZaloRepository.AddAsync(userNhomZalo);

            // Save changes
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

    }
}
