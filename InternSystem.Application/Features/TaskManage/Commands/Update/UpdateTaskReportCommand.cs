using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;

namespace InternSystem.Application.Features.TaskManage.Commands.Update
{
    public class UpdateTaskReportValidator : AbstractValidator<UpdateTaskReportCommand>
    {
        public UpdateTaskReportValidator()
        {
            RuleFor(m => m.Id).NotEmpty();
        

        }
    }

    public class UpdateTaskReportCommand : IRequest<TaskReportResponse>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public string MoTa { get; set; }
        public string NoiDungBaoCao { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayBaoCao { get; set; } = DateTime.Now;
        public string? LastUpdatedBy { get; set; }

    }
}
