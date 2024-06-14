using FluentValidation;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Handlers;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Commands
{
    public class CreateCauHoiCongNgheValidator :AbstractValidator<CreateCauHoiCongNgheCommand>
    {
        public CreateCauHoiCongNgheValidator() 
        {
            RuleFor(x => x.IdCauHoi).NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.IdCongNghe).NotEmpty()
                .GreaterThan(0);
        }
    }
    public class CreateCauHoiCongNgheCommand : IRequest<CreateCauHoiCongNgheResponse>
    {
        public int IdCongNghe { get; set; }
        public int IdCauHoi { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
}
