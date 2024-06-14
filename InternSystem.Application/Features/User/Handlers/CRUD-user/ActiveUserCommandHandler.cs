using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Commands.User;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers.CRUD_user
{
    public class ActiveUserCommandHandler : IRequestHandler<ActiveUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AspNetUser> _userManager;
        public ActiveUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AspNetUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<bool> Handle(ActiveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.UserId} not found.");
            }
            
            user.IsActive = request.IsActive;
            user.LastUpdatedTime = DateTime.Now;

            if (!request.IsActive)
            {
                user.IsDelete = true;
                user.DeletedTime = DateTime.Now;
            }
            else
            {
                user.IsDelete = false;
                user.DeletedTime = null;
            }

            var userUpdate = _mapper.Map<AspNetUser>(user);
            var result = await _userManager.UpdateAsync(userUpdate);
            return result.Succeeded;
        }
    }
}
