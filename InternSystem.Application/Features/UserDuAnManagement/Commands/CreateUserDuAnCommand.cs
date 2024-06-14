using FluentValidation;
using InternSystem.Application.Features.UserDuAnManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserDuAnManagement.Commands
{
    public class CreateUserDuAnValidator : AbstractValidator<CreateUserDuAnCommand>
    {
        public CreateUserDuAnValidator() {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.DuAnId).GreaterThan(0);
            RuleFor(m => m.IdViTri).GreaterThan(0);
        }
    }
    public class CreateUserDuAnCommand : IRequest<CreateUserDuAnResponse>
    {
        public string? UserId { get; set; }
        public int DuAnId { get; set; }
        public int IdViTri { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
}
