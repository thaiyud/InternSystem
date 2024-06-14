using FluentValidation;
using MediatR;

namespace InternSystem.Application.Features.TruongHocManagement.Commands
{

    public class DeleteTruongHocValidator : AbstractValidator<DeleteTruongHocCommand>
    {
        public DeleteTruongHocValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteTruongHocCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? DeletedBy { get; set; }
    }
}
