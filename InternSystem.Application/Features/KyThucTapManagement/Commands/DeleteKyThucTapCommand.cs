using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.KyThucTapManagement.Commands
{
    public class DeleteKyThucTapValidator : AbstractValidator<DeleteKyThucTapCommand>
    {
        public DeleteKyThucTapValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteKyThucTapCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? DeletedBy { get; set; }
    }
}
