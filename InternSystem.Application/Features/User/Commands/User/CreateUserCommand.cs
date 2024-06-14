using FluentValidation;
using InternSystem.Application.Features.User.Models.UserModels;
using MediatR;

namespace InternSystem.Application.Features.User.Commands.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(model => model.HoVaTen)
            .NotEmpty().WithMessage("Full name is required")
            .MinimumLength(2).WithMessage("Full name must be at least 2 characters long");

            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(model => model.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Length(10).WithMessage("Phone number must be 10 digits long");

            RuleFor(model => model.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(6).WithMessage("Username must be at least 6 characters long");

            RuleFor(model => model.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .MaximumLength(20).WithMessage("Password must not exceed 20 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                .Matches("[!@#$%^&*]").WithMessage("Password must contain at least one special character");

            RuleFor(model => model.InternInfoId)
                .GreaterThan(0).WithMessage("Intern Info ID must be greater than 0");

            RuleFor(model => model.RoleName)
                .NotEmpty().WithMessage("Role Name is required");
        }
    }
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public required string HoVaTen { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        public int? InternInfoId { get; set; }
        public required string RoleName { get; set; }
    }
}
