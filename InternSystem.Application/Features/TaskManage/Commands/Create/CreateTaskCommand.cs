using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;


namespace InternSystem.Application.Features.TaskManage.Commands.Create
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
            RuleFor(m => m.DuAnId).NotEmpty();
            RuleFor(m => m.MoTa).NotEmpty();
            RuleFor(m => m.NoiDung).NotEmpty();
            RuleFor(m => m.NgayGiao).LessThan(m => m.HanHoanThanh);

        }
    }

    public class CreateTaskCommand : IRequest<TaskResponse>
    {
        public int DuAnId { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayGiao { get; set; }
        public DateTime? HanHoanThanh { get; set; }
        public string? CreatedBy { get; set; }

    }

}
