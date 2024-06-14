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
    public class GetNhomZaloTaskByIdValidator : AbstractValidator<NhomZaloTaskReponse>
    {
        public GetNhomZaloTaskByIdValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetNhomZaloTaskByIdQuery : IRequest<NhomZaloTaskReponse>
    {
        public int Id { get; set; }
    }

}
