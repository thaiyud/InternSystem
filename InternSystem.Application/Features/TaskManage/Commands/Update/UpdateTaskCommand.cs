using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;

namespace InternSystem.Application.Features.TaskManage.Commands.Update
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        {
            RuleFor(m => m.Id).NotEmpty();

        }
    }

    public class UpdateTaskCommand : IRequest<TaskResponse>
    {
        public int Id { get; set; }
        public int? DuAnId { get; set; }
        public string? MoTa { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? NgayGiao { get; set; }
        public DateTime? HanHoanThanh { get; set; }

        public bool? HoanThanh { get; set; } = false;
        public string? LastUpdatedBy { get; set; }

    }
}
