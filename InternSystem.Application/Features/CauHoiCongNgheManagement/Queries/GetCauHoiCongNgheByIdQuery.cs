using FluentValidation;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Queries {

    public class GetCauHoiCongNgheByIdValidator : AbstractValidator<GetCauHoiCongNgheByIdQuery>
{
    public GetCauHoiCongNgheByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
            .GreaterThan(0);
    }
}
    public class GetCauHoiCongNgheByIdQuery : IRequest<GetCauHoiCongNgheByIdResponse>
    {
        public int Id { get; set; }
    }
}
