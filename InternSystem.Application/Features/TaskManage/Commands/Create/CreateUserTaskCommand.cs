using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Commands.Create
{
    public class CreateUserTaskValidator : AbstractValidator<CreateUserTaskCommand>
    {
        public CreateUserTaskValidator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.TaskId).NotEmpty();
        }
    }

    public class CreateUserTaskCommand : IRequest<UserTaskReponse>
    {
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public string? CreatedBy { get; set; }

    }
}
