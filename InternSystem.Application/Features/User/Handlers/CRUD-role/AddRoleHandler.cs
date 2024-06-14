using MediatR;
using Microsoft.AspNetCore.Identity;
using static InternSystem.Application.Features.User.Commands.AddRoleCommandValidation;

namespace InternSystem.Application.Features.User.Handlers
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, bool>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AddRoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {

            var result = await _roleManager.CreateAsync(new IdentityRole(request.Name.ToLower()));
            return result.Succeeded;

        }
    }
}
