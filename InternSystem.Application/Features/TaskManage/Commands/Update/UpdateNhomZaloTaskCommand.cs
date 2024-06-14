using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Commands.Update
{
    public class UpdateNhomZaloTaskValidator : AbstractValidator<UpdateNhomZaloTaskCommand>
    {
        public UpdateNhomZaloTaskValidator()
        {
            RuleFor(m => m.Id).NotEmpty();

        }
    }

    public class UpdateNhomZaloTaskCommand : IRequest<TaskResponse>
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int NhomZaloId { get; set; }
        public string? TrangThai { get; set; }
        public string LastUpdatedBy { get; set; }

    }
}
