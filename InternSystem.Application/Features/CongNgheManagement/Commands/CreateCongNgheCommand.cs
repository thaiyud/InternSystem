using FluentValidation;
using InternSystem.Application.Features.CongNgheManagement.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace InternSystem.Application.Features.CongNgheManagement.Commands
{
    public class CreateCongNgheValidator : AbstractValidator<CreateCongNgheCommand>
    {
        public CreateCongNgheValidator()
        {
            RuleFor(m => m.Ten).NotEmpty();
        }
    }
    public class CreateCongNgheCommand : IRequest<CreateCongNgheResponse>
    {
        public string Ten { get; set; }
        public int IdViTri { get; set; }
        public string UrlImage { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
}
