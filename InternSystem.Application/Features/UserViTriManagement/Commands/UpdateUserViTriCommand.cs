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
    public class UpdateUserViTriValidator : AbstractValidator<UpdateUserViTriCommand>
    {
        public UpdateUserViTriValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }
    public class UpdateUserViTriCommand : IRequest<UpdateUserViTriResponse>
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int IdViTri { get; set; }
        [JsonIgnore]
        public string? LastUpdatedBy { get; set; }
    }
}
