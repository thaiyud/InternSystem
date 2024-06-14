using FluentValidation;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Queries
{
    public class GetLichPhongVanByIdValidator : AbstractValidator<GetLichPhongVanByIdQuery>
    {
        public GetLichPhongVanByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0); // ID phải lớn hơn 0
        }
    }

    public class GetLichPhongVanByIdQuery : IRequest<GetLichPhongVanByIdResponse>
    {
        public int Id { get; set; }
    }
}

