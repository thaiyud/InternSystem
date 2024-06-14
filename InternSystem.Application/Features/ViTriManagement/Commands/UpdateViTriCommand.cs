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
    public class UpdateViTriValidator : AbstractValidator<UpdateViTriCommand>
    {
        public UpdateViTriValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }
    public class UpdateViTriCommand : IRequest<UpdateViTriResponse>
    {
        public int Id { get; set; } 
        public string? Ten { get; set; }
        public string? LinkNhomZalo { get; set; }
        public int DuAnId { get; set; }
        [JsonIgnore]
        public string? LastUpdatedBy { get; set; }
    }
}
