using FluentValidation;

using InternSystem.Application.Features.TaskManage.Models;
using MediatR;


namespace InternSystem.Application.Features.TaskManage.Commands.Create
{
    public class CreateTaskReportValidator : AbstractValidator<CreateTaskReportCommand>
    {
        public CreateTaskReportValidator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.TaskId).NotEmpty();
            RuleFor(m => m.MoTa).NotEmpty();
            RuleFor(m => m.NoiDungBaoCao).NotEmpty();

        }
    }

    public class CreateTaskReportCommand : IRequest<TaskReportResponse>
    {
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public string MoTa { get; set; }
        public string NoiDungBaoCao { get; set; }
        public DateTime NgayBaoCao { get; set; }
        public string? CreatedBy { get; set; }

    }
}
