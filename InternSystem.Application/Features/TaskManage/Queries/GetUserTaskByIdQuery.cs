using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Queries
{
    public class GetUserTaskByIdValidator : AbstractValidator<GetUserTaskByIdQuery>
    {
        public GetUserTaskByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetUserTaskByIdQuery : IRequest<UserTaskReponse>
    {
        public int Id { get; set; }
    }
}
