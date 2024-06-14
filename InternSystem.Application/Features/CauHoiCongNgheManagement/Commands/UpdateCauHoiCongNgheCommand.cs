using FluentValidation;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Commands
{
    public class UpdateCauHoiCongNgheValidator : AbstractValidator<UpdateCauHoiCongNgheCommand>
    {
        public UpdateCauHoiCongNgheValidator() 
        {
            RuleFor(x => x.Id).NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.IdCongNghe).NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.IdCauHoi).NotEmpty()
                .GreaterThan(0);
        }
    }
    public class UpdateCauHoiCongNgheCommand : IRequest<UpdateCauHoiCongNgheResponse>
    {
        public int Id { get; set; }
        public int IdCongNghe { get; set; }
        public int IdCauHoi { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
