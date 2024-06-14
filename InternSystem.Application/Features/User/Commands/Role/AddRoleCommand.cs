using FluentValidation;
using MediatR;
using static InternSystem.Application.Features.User.Commands.AddRoleCommandValidation;

namespace InternSystem.Application.Features.User.Commands
{
    public class AddRoleCommandValidation : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidation()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .MinimumLength(1);
        }
        public class AddRoleCommand : IRequest<bool>
        {
            public string? Name { get; set; }
        }
    }
}
