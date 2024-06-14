using FluentValidation;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Commands
{

    public class DeleteInternInfoValidator : AbstractValidator<DeleteInternInfoCommand>
    {
        public DeleteInternInfoValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteInternInfoCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string? DeletedBy { get; set; }
    }
}
