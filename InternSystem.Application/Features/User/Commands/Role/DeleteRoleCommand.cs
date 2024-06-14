using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InternSystem.Application.Features.User.Commands.Role.DeleteRoleCommandValidation;

namespace InternSystem.Application.Features.User.Commands.Role
{
    public class DeleteRoleCommandValidation : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidation()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .MinimumLength(1);
        }
        public class DeleteRoleCommand : IRequest<bool>
        {
            public string? Name { get; set; }
        }
    }
}
