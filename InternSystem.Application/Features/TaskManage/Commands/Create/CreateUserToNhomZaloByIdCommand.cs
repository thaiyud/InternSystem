using FluentValidation;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Commands.Create
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
        public string? CreateBy { get; set; }
    }
}
