using FluentValidation;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Commands
{
    public class DeleteEmailUserStatusCommandValidator : AbstractValidator<DeleteEmailUserStatusCommand>
    {
        public DeleteEmailUserStatusCommandValidator()
        {
            RuleFor(m => m.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }

    public class DeleteEmailUserStatusCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
