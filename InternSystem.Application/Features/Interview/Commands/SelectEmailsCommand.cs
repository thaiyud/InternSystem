using FluentValidation;
using InternSystem.Application.Common.EmailService;
using MediatR;

namespace InternSystem.Application.Features.Interview.Commands
{
    public class SelectEmailsCommandValidator : AbstractValidator<SelectEmailsCommand>
    {
        private readonly IEmailService _emailService;

        public SelectEmailsCommandValidator(IEmailService emailService)
        {
            _emailService = emailService;

            RuleFor(model => model.Indices)
            .NotEmpty().WithMessage("Indices cannot be empty.")
            .Must(BeValidIndices).WithMessage("Indices must be non-negative integers and less than the length of available emails.");
        }

        private bool BeValidIndices(List<int> indices)
        {
            foreach (var index in indices)
            {
                if (index < 0 || index >= _emailService.GetAvailableEmails().Count)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class SelectEmailsCommand : IRequest<IEnumerable<string>>
    {
        public List<int> Indices { get; set; }

        public SelectEmailsCommand(List<int> indices)
        {
            Indices = indices;
        }
    }
}
