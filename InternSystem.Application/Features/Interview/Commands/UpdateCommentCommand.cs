using FluentValidation;
using InternSystem.Application.Features.Interview.Models;
using MediatR;

namespace InternSystem.Application.Features.Interview.Commands
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(m => m.Content)
                .NotEmpty().WithMessage("Content must not be empty.");

            RuleFor(m => m.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
    public class UpdateCommentCommand : IRequest<GetDetailCommentResponse>
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
