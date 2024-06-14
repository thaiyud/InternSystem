using FluentValidation;
using InternSystem.Application.Features.UserDuAnManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserDuAnManagement.Commands
{
    public class UpdateUserDuAnValidator : AbstractValidator<UpdateUserDuAnCommand>
    {
        public UpdateUserDuAnValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }
    public class UpdateUserDuAnCommand : IRequest<UpdateUserDuAnResponse>
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int DuAnId { get; set; }
        public int IdViTri { get; set; }
        [JsonIgnore]
        public string? LastUpdatedBy { get; set; }
    }
}
