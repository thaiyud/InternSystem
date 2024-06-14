using FluentValidation;
using InternSystem.Application.Features.CauHoiManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiManagement.Commands
{
    public class CreateCauHoiValidator : AbstractValidator<CreateCauHoiCommand>
    {
        public CreateCauHoiValidator()
        {
            RuleFor(x => x.NoiDung).NotEmpty();
        }
    }
    public class CreateCauHoiCommand : IRequest<CreateCauHoiResponse>
    {
        public string NoiDung { get; set; }
        public string? CreatedBy { get; set; }
    }
}
