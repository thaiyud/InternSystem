using FluentValidation;
using InternSystem.Application.Features.Interview.Models;
using MediatR;

namespace InternSystem.Application.Features.Interview.Commands
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(m => m.Content)
                .NotEmpty().WithMessage("Content must not be empty.");

            RuleFor(m => m.IdNguoiDuocComment)
                .GreaterThan(0).WithMessage("IdNguoiDuocComment must be greater than 0.");
        }
    }

    public class CreateCommentCommand : IRequest<GetDetailCommentResponse>
    {
        public string Content { get; set; }
        public int IdNguoiDuocComment { get; set; }
    }
}
