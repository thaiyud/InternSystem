using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;


namespace InternSystem.Application.Features.TaskManage.Commands.Update
{
    public class UpdateUserTaskValidator : AbstractValidator<UpdateUserTaskCommand>
    {
        public UpdateUserTaskValidator()
        {
            RuleFor(m => m.Id).NotEmpty();

        }
    }

    public class UpdateUserTaskCommand : IRequest<UserTaskReponse>
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int? TaskId { get; set; }
        public string? TrangThai { get; set; }
        public string?   LastUpdatedBy { get; set; }


    }
}
