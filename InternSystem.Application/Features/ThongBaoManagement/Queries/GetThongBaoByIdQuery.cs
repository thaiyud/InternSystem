using FluentValidation;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Queries
{

    public class GetThongBaoByIdValidator : AbstractValidator<GetThongBaoByIdQuery>
    {
        public GetThongBaoByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetThongBaoByIdQuery : IRequest<GetThongBaoByIdResponse>
    {
        public int Id { get; set; } 
    }
}
