using FluentValidation;
using MediatR;


namespace InternSystem.Application.Features.TaskManage.Commands.Delete
{
    public class DeleteTaskReportValidator : AbstractValidator<DeleteTaskReportCommand>
    {
        public DeleteTaskReportValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteTaskReportCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string? DeletedBy { get; set; }
    }
}
