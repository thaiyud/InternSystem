using FluentValidation;

using MediatR;


namespace InternSystem.Application.Features.TaskManage.Commands.Delete
{
    public class DeleteUserTaskValidator : AbstractValidator<DeleteUserTaskCommand>
    {
        public DeleteUserTaskValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteUserTaskCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string? DeletedBy { get; set; }
    }
}
