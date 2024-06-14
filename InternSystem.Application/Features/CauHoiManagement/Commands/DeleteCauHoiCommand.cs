using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiManagement.Commands
{
    public class DeleteCauHoiValidator : AbstractValidator<DeleteCauHoiCommand>
    {
        public DeleteCauHoiValidator() 
        {
            RuleFor(x => x.Id).NotEmpty()
                .GreaterThan(0);
        }
    }
    public class DeleteCauHoiCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string? DeletedBy { get; set; }
    }
}
