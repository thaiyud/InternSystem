using FluentValidation;
using InternSystem.Application.Features.CauHoiManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiManagement.Queries
{
    public class GetCauHoiByIdValidator : AbstractValidator<GetCauHoiByIdQuery>
    {
        public GetCauHoiByIdValidator() 
        {
            RuleFor(x => x.Id).NotEmpty()
                .GreaterThan(0);
        }
    }
    public class GetCauHoiByIdQuery : IRequest<GetCauHoiByIdResponse>
    {
        public int Id { get; set; }
    }
}
