using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Commands
{
    public class DeleteLichPhongVanValidator : AbstractValidator<DeleteLichPhongVanCommand>
    {
        public DeleteLichPhongVanValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0); // ID phải lớn hơn 0
        }
    }


    public class DeleteLichPhongVanCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string? DeletedBy { get; set; }
    }
}
