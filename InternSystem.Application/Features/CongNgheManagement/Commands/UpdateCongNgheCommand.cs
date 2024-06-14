using FluentValidation;
using InternSystem.Application.Features.CongNgheManagement.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace InternSystem.Application.Features.CongNgheManagement.Commands
{
    public class UpdateCongNgheValidator : AbstractValidator<UpdateCongNgheCommand>
    {
        public UpdateCongNgheValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }


    public class UpdateCongNgheCommand : IRequest<UpdateCongNgheResponse>
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int IdViTri { get; set; }
        public string UrlImage { get; set; }
        [JsonIgnore]
        public string? LastUpdatedBy { get; set; }
    }
}
