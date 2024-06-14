using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InternSystem.Application.Features.User.Commands.AddRoleCommandValidation;
using static InternSystem.Application.Features.User.Commands.UpdateRoleCommandValidation;

namespace InternSystem.Application.Features.User.Handlers.CRUD_role
{
  
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public UpdateRoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role == null)
            {
                return false; // Role not found
            }

            role.Name = request.NewRoleName.ToLower();
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;

        }
    }
}
