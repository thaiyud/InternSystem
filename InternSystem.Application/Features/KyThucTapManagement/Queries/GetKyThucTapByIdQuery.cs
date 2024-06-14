using FluentValidation;
using InternSystem.Application.Features.KyThucTapManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.KyThucTapManagement.Queries
{

    public class GetKyThucTapByIdValidator : AbstractValidator<GetKyThucTapByIdQuery>
    {
        public GetKyThucTapByIdValidator()
        {
            RuleFor(m => m.Id).NotEmpty();
        }
    }

    public class GetKyThucTapByIdQuery : IRequest<GetKyThucTapByIdResponse>
    {
        public int Id { get; set; }
    }
}
