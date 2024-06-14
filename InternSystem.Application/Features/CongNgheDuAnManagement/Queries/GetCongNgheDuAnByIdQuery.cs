using FluentValidation;
using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Queries
{
    public class GetCongNgheDuAnByIdValidator : AbstractValidator<GetCongNgheDuAnByIdQuery>
    {
        public GetCongNgheDuAnByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetCongNgheDuAnByIdQuery : IRequest<GetCongNgheDuAnByIdResponse>
    {
        public int Id { get; set; }
    }
}
