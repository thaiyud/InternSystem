using FluentValidation;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Application.Features.DuAnManagement.Queries;
using InternSystem.Application.Features.Interview.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Interview.Queries
{
    public class GetCommentByIdQueryValidator : AbstractValidator<GetCommentByIdQuery>
    {
        public GetCommentByIdQueryValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetCommentByIdQuery : IRequest<GetDetailCommentResponse>
    {
        public int Id { get; set; }
    }
}
