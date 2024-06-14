using FluentValidation;
using InternSystem.Application.Features.CongNgheManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheManagement.Queries
{
    public class GetCongNgheByIdValidator : AbstractValidator<GetCongNgheByIdQuery>
    {
        public GetCongNgheByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetCongNgheByIdQuery : IRequest<GetCongNgheByIdResponse>
    {
        public int Id { get; set; }
    }
}
