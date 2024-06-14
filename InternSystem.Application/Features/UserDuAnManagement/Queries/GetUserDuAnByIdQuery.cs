using FluentValidation;
using InternSystem.Application.Features.UserDuAnManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserDuAnManagement.Queries
{

    public class GetUserDuAnByIdValidator : AbstractValidator<GetUserDuAnByIdQuery>
    {
        public GetUserDuAnByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetUserDuAnByIdQuery : IRequest<GetUserDuAnByIdResponse>
    {
        public int Id { get; set; }
    }
}  
