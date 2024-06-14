using FluentValidation;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Commands
{
    public class DeleteThongBaoValidator : AbstractValidator<DeleteThongBaoCommand>
    {
        public DeleteThongBaoValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteThongBaoCommand : IRequest<DeleteThongBaoResponse>
    {
        public int Id { get; set; }
    }
}
