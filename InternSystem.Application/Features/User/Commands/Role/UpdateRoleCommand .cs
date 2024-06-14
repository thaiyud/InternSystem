using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InternSystem.Application.Features.User.Commands.UpdateRoleCommandValidation;

namespace InternSystem.Application.Features.User.Commands
{
    public class UpdateRoleCommandValidation : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidation()
        {
            RuleFor(model => model.RoleId)
                .NotEmpty()
                .MinimumLength(1);
            RuleFor(model => model.NewRoleName)
                .NotEmpty()
                .MinimumLength(1);
        }
        public class UpdateRoleCommand : IRequest<bool>
        {
            public string RoleId { get; set; }
            public string NewRoleName { get; set; }

            public UpdateRoleCommand(string roleId, string newRoleName)
            {
                RoleId = roleId;
                NewRoleName = newRoleName;
            }
        }
    }
}
