using FluentValidation;
using InternSystem.Application.Features.TruongHocManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.TruongHocManagement.Commands
{
    public class UpdateTruongHocValidator : AbstractValidator<UpdateTruongHocCommand>
    {
        public UpdateTruongHocValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);

        }
    }

    public class UpdateTruongHocCommand : IRequest<UpdateTruongHocResponse>
    {
        public int Id { get; set; }
        public string? Ten { get; set; }
        public int? SoTuanThucTap { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
