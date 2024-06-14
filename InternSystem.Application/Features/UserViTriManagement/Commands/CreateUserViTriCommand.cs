using FluentValidation;
using InternSystem.Application.Features.UserViTriManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserViTriManagement.Commands
{
    public class CreateUserViTriValidator : AbstractValidator<CreateUserViTriCommand>
    {
        public CreateUserViTriValidator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.IdViTri).GreaterThan(0);
        }
    }
    public class CreateUserViTriCommand : IRequest<CreateUserViTriResponse>
    {
        public string? UserId { get; set; }
        public int IdViTri { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
}
