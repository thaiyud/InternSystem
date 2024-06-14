using InternSystem.Application.Features.User.Commands.Role;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InternSystem.Application.Features.User.Commands.Role.DeleteRoleCommandValidation;

namespace InternSystem.Application.Features.User.Handlers.CRUD_role
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand,bool>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public DeleteRoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByNameAsync(request.Name.ToLower());
            if (role == null)
            {
                return false; // Role not found
            }

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;

        }
    }
}
