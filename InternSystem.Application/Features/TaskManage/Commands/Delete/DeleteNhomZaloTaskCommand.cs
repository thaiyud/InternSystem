using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Commands.Delete
{
    public class DeleteNhomZaloTaskValidator : AbstractValidator<DeleteNhomZaloTaskCommand>
    {
        public DeleteNhomZaloTaskValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteNhomZaloTaskCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string? DeletedBy { get; set; }
    }
}
