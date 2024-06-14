using FluentValidation;
using MediatR;

namespace InternSystem.Application.Features.Interview.Commands
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(m => m.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }

    public class DeleteCommentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
