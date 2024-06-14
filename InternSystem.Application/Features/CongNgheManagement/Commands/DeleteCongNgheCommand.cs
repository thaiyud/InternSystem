using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheManagement.Commands
{
    public class DeleteCongNgheValidator : AbstractValidator<DeleteCongNgheCommand>
    {
        public DeleteCongNgheValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class DeleteCongNgheCommand : IRequest<bool>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string? DeletedBy { get; set; }
    }
}
