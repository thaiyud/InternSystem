using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.Comunication.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class UpdateUserNhomZaloCommandHandler : IRequestHandler<UpdateUserNhomZaloCommandWrapper, UpdateUserNhomZaloResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserNhomZaloCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateUserNhomZaloResponse> Handle(UpdateUserNhomZaloCommandWrapper request, CancellationToken cancellationToken)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userNhomZalo = await _unitOfWork.UserNhomZaloRepository.GetByIdAsync(request.Id);

            if (userNhomZalo == null)
            {
                return new UpdateUserNhomZaloResponse { IsSuccessful = false, ErrorMessage = "UserNhomZalo not found." };
            }

            userNhomZalo.IsMentor = request.Command.isMentor;
            userNhomZalo.LastUpdatedBy = currentUserId;
            userNhomZalo.LastUpdatedTime = DateTimeOffset.Now;

            try
            {
                _unitOfWork.UserNhomZaloRepository.UpdateUserNhomZaloAsync(userNhomZalo);
                await _unitOfWork.SaveChangeAsync();
                return new UpdateUserNhomZaloResponse { IsSuccessful = true, Id = userNhomZalo.Id };
            }
            catch (Exception ex)
            {
                // Log the exception
                return new UpdateUserNhomZaloResponse { IsSuccessful = false, ErrorMessage = $"Failed to update NhomZalo: {ex.Message}" };
            }
        }
    }
}