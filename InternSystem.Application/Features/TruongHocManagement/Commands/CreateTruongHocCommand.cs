using FluentValidation;
using InternSystem.Application.Features.TruongHocManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.TruongHocManagement.Commands
{
    public class CreateTruongHocValidator : AbstractValidator<CreateTruongHocCommand>
    {
        public CreateTruongHocValidator()
        {
            RuleFor(m => m.Ten).NotEmpty();
            RuleFor(m => m.SoTuanThucTap).GreaterThan(0).LessThanOrEqualTo(14);
        }
    }

    public class CreateTruongHocCommand : IRequest<CreateTruongHocResponse>
    {
        public string Ten { get; set; }
        public int SoTuanThucTap { get; set; }
        public string? CreatedBy { get; set; }
    }
}
