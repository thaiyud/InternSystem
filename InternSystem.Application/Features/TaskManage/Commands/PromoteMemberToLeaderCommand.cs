using FluentValidation;
using InternSystem.Application.Features.TaskManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Commands
{
    public class PromoteMemberValidator : AbstractValidator<PromoteMemberToLeaderCommand>
    {
        public PromoteMemberValidator()
        {
            RuleFor(m => m.MemberId).NotEmpty();
            RuleFor(m => m.NhomZaloId).NotEmpty();
            RuleFor(m => m.DuanId).NotEmpty();

        }
    }

    public class PromoteMemberToLeaderCommand : IRequest<ExampleResponse>
    {
        public string MemberId { get; set; }
        public int NhomZaloId { get; set; }
        public int DuanId { get; set; }

    }
  
}
