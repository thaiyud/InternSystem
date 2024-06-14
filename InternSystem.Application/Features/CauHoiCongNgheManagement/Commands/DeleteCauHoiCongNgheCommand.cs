using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Commands
{
    public class DeleteCauHoiCongNgheValidator : AbstractValidator<DeleteCauHoiCongNgheCommand>
    {
        public DeleteCauHoiCongNgheValidator() 
        {
            RuleFor(x => x.Id).NotEmpty()
                .GreaterThan(0);
        }
    }
    public class DeleteCauHoiCongNgheCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? DeletedBy { get; set; }
    }
}
