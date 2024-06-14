using FluentValidation;
using MediatR;

namespace InternSystem.Application.Features.PhongVanManagement.Commands
{
    public class DeletePhongVanValidator : AbstractValidator<DeletePhongVanCommand>
    {
        public DeletePhongVanValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }
    public class DeletePhongVanCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? DeletedBy { get; set; }
    }
}
