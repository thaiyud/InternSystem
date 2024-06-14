using FluentValidation;

using MediatR;


namespace InternSystem.Application.Features.TaskManage.Commands.Delete
{
    public class DeleteTaskValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteTaskCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string? DeletedBy { get; set; }
    }
}
