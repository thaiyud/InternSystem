using FluentValidation;
using InternSystem.Application.Features.TasksAndReports.TaskManagement.Models;
using MediatR;


namespace InternSystem.Application.Features.TasksAndReports.TaskManagement.Queries
{
    public class GetTaskByMotaQueryValidator : AbstractValidator<GetTaskByMoTaQuery>
    {
        public GetTaskByMotaQueryValidator()
        {
            RuleFor(model => model.mota).NotEmpty().WithMessage("Mota is required");
        }
    }

    public class GetTaskByMoTaQuery : IRequest<IEnumerable<TaskResponse>>
    {
        public string mota { get; set; } = string.Empty;
    }
}
