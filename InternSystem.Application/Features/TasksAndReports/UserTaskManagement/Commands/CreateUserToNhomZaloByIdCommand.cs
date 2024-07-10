using FluentValidation;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Commands
{
    public class GetKetQuaPhongVanByIdValidator : AbstractValidator<UserNhomZaloTaskResponse>
    {
        public GetKetQuaPhongVanByIdValidator()
        {
            RuleFor(m => m.InterviewId).GreaterThan(0); // ID phải lớn hơn 0
        }
    }

    public class CreateUserToNhomZaloByIdCommand : IRequest<UserNhomZaloTaskResponse>
    {
        public int Id { get; set; }
    }
}
