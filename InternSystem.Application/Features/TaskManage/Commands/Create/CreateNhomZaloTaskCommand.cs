using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Commands.Create
{
    public class CreateNhomZaloTaskValidator : AbstractValidator<CreateNhomZaloTaskCommand>
    {
        public CreateNhomZaloTaskValidator()
        {
            RuleFor(m => m.TaskId).NotEmpty();
            RuleFor(m => m.NhomZaloId).NotEmpty();
        }
    }

    public class CreateNhomZaloTaskCommand : IRequest<NhomZaloTaskReponse>
    {
        public int TaskId { get; set; }
        public int NhomZaloId { get; set; }
        public string? TrangThai { get; set; }
        public string? CreatedBy { get; set; }

    }
}
