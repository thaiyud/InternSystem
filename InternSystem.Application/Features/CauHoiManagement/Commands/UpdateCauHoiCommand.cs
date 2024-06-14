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
    public class UpdateCauHoiValidator : AbstractValidator<UpdateCauHoiCommand>
    {
        public UpdateCauHoiValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.NoiDung).NotEmpty();
        }
    }
    public class UpdateCauHoiCommand : IRequest<UpdateCauHoiResponse>
    {
        public int Id { get; set; }
        public string NoiDung {  get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
