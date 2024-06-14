using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternSystem.Application.Features.User.Handlers
{
    public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand, bool>
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddUserToRoleCommandHandler(UserManager<AspNetUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
            if (!roleExists)
            {
                throw new Exception("Role not found");
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            return result.Succeeded;
        }
    }
}
