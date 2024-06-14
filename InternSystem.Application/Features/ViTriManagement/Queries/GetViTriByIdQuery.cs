using FluentValidation;
using InternSystem.Application.Features.UserDuAnManagement.Models;
using InternSystem.Application.Features.ViTriManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ViTriManagement.Queries
{
    public class GetViTriByIdValidator : AbstractValidator<GetViTriByIdQuery>
    {
        public GetViTriByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetViTriByIdQuery : IRequest<GetViTriByIdResponse>
    {
        public int Id { get; set; }
    }
}
