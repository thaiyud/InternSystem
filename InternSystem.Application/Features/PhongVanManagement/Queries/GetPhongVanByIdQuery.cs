using FluentValidation;
using InternSystem.Application.Features.PhongVanManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Queries
{
    public class GetPhongVanByIdValidator : AbstractValidator<GetPhongVanByIdQuery>
    {
        public GetPhongVanByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0); //
        }
    }

    public class GetPhongVanByIdQuery : IRequest<GetPhongVanByIdResponse>
    {
        public int Id { get; set; }
    }
}

