using FluentValidation;
using InternSystem.Application.Features.DuAnManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DuAnManagement.Queries
{

    public class GetDuAnByIdValidator : AbstractValidator<GetDuAnByIdQuery>
    {
        public GetDuAnByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetDuAnByIdQuery : IRequest<GetDuAnByIdResponse>
    {
        public int Id { get; set; }
    }
}  
