using FluentValidation;
using InternSystem.Application.Features.User.Models.UserModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Commands
{

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(model => model.Id)
                .NotEmpty().WithMessage("User ID is required"); ;

            RuleFor(model => model.HoVaTen)
            .MinimumLength(2).WithMessage("Full name must be at least 2 characters long");

            RuleFor(model => model.Email)
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(model => model.PhoneNumber)
                .Length(10).WithMessage("Phone number must be 10 digits long");
        }
    }
    public class UpdateUserCommand : IRequest<CreateUserResponse>
    {
        public required string Id { get; set; }
        public string? HoVaTen { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? InternInfoId { get; set; }
        [JsonIgnore]
        public string? RoleName { get; set; }
    }
}
