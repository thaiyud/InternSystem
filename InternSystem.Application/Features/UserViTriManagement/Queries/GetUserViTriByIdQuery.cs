using FluentValidation;
using InternSystem.Application.Features.UserViTriManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserViTriManagement.Queries
{
    public class GetUserViTriByIdValidator : AbstractValidator<GetUserViTriByIdQuery>
    {
        public GetUserViTriByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetUserViTriByIdQuery : IRequest<GetUserViTriByIdResponse>
    {
        public int Id { get; set; }
    }
}
