using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserDuAnManagement.Commands
{
    public class DeleteUserDuAnValidator : AbstractValidator<DeleteUserDuAnCommand>
    {
        public DeleteUserDuAnValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteUserDuAnCommand : IRequest<bool>
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string? DeletedBy { get; set; }
    }
}
