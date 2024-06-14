using FluentValidation;
using InternSystem.Application.Features.InternManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Commands
{
    public class CreateEmailUserStatusCommandValidator : AbstractValidator<CreateEmailUserStatusCommand>
    {
        public CreateEmailUserStatusCommandValidator()
        {
            RuleFor(m => m.IdNguoiNhan)
                .GreaterThan(0).WithMessage("IdNguoiDuocComment must be greater than 0.");

            RuleFor(m => m.EmailLoai1)
            .NotEmpty().WithMessage("EmailLoai1 must not be empty.")
            .EmailAddress().WithMessage("EmailLoai1 must be a valid email address.");

            RuleFor(m => m.EmailLoai2)
                .NotEmpty().WithMessage("EmailLoai2 must not be empty.")
                .EmailAddress().WithMessage("EmailLoai2 must be a valid email address.");

            RuleFor(m => m.EmailLoai3)
                .NotEmpty().WithMessage("EmailLoai3 must not be empty.")
                .EmailAddress().WithMessage("EmailLoai3 must be a valid email address.");
        }
    }

    public class CreateEmailUserStatusCommand : IRequest<GetDetailEmailUserStatusResponse>
    {
        public int IdNguoiNhan { get; set; }
        public string EmailLoai1 { get; set; }
        public string EmailLoai2 { get; set; }
        public string EmailLoai3 { get; set; }
    }
}
