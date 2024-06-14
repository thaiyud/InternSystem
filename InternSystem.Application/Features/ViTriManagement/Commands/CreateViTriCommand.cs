using FluentValidation;
using InternSystem.Application.Features.ViTriManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ViTriManagement.Commands
{
    public class CreateViTriValidator : AbstractValidator<CreateViTriCommand>
    {
        public CreateViTriValidator()
        {
            RuleFor(m => m.Ten).NotEmpty();
            RuleFor(m => m.DuAnId).GreaterThan(0);
        }
    }
    public class CreateViTriCommand : IRequest<CreateViTriResponse>
    {
        public string? Ten { get; set; }
        public string? LinkNhomZalo { get; set; }
        public int DuAnId { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
}
