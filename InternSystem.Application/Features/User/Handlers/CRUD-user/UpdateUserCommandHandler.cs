using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Commands.User;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers.CRUD_user
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AspNetUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<CreateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = await _userManager.FindByIdAsync(request.Id);

            if (newUser == null)
                throw new ArgumentNullException(nameof(request.Id), "User not found");

            if (!string.IsNullOrEmpty(request.HoVaTen))
                newUser.HoVaTen = request.HoVaTen;


            if (!string.IsNullOrEmpty(request.Email) && request.Email != newUser.Email)
            {
                newUser.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber) && request.PhoneNumber != newUser.PhoneNumber)
            {
                newUser.PhoneNumber = request.PhoneNumber;
            }

            if (request.InternInfoId != null)
            {
                if (await _unitOfWork.InternInfoRepository.GetByIdAsync(request.InternInfoId) == null)
                    throw new ArgumentNullException(nameof(request.InternInfoId), "Intern information not found");

                newUser.InternInfoId = request.InternInfoId;
            }

            AspNetUser userUpdate = _mapper.Map<AspNetUser>(newUser);
            userUpdate.LastUpdatedTime = DateTime.Now;
            var resultUpdate = await _userManager.UpdateAsync(userUpdate);

            if (!resultUpdate.Succeeded)
                throw new InvalidOperationException(resultUpdate.Errors.FirstOrDefault()?.Description);
            //=====================================================================================================
            // Get current roles
            var currentRoles = await _userManager.GetRolesAsync(newUser);

            // Find new role
            if (!string.IsNullOrEmpty(request.RoleName))
            {
                var roleName = request.RoleName.Trim();
                var newRole = await _roleManager.FindByNameAsync(roleName);
                if (newRole == null)
                {
                    throw new ArgumentException("Role not found");
                }

                // Remove current roles
                var removeResult = await _userManager.RemoveFromRolesAsync(newUser, currentRoles);
                if (!removeResult.Succeeded)
                {
                    throw new InvalidOperationException($"Error removing user roles: {removeResult.Errors.FirstOrDefault()?.Description}");
                }

                // Add new role
                var addResult = await _userManager.AddToRoleAsync(newUser, newRole.Name);
                if (!addResult.Succeeded)
                {
                    throw new InvalidOperationException($"Error adding user to role: {addResult.Errors.FirstOrDefault()?.Description}");
                }
            }
            //=====================================================================================================
            return _mapper.Map<CreateUserResponse>(userUpdate);
        }

    }
}
