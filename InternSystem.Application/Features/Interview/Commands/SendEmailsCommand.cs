using FluentValidation;
using InternSystem.Application.Common.EmailService;
using MediatR;

namespace InternSystem.Application.Features.Interview.Commands
{
    public class SendEmailsCommandValidator : AbstractValidator<SendEmailsCommand>
    {
        public SendEmailsCommandValidator(IEmailService emailService)
        {
            RuleFor(model => model.Subject)
            .NotEmpty().WithMessage("Subject cannot be empty.");

            RuleFor(model => model.Body)
            .NotEmpty().WithMessage("Body cannot be empty.");

            RuleFor(model => model.EmailType)
            .NotEmpty().WithMessage("Email type cannot be empty.")
            .Must(BeValidEmailType).WithMessage("Invalid email type.");
        }

        private bool BeValidEmailType(string emailType)
        {
            var validEmailTypes = new List<string> { "Interview Date", "Interview Result", "Internship Time", "Internship Information" };
            return validEmailTypes.Contains(emailType);
        }
    }

    public class SendEmailsCommand : IRequest<bool>
    {
        public IEnumerable<string> SelectedEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailType { get; set; }

        public SendEmailsCommand(IEnumerable<string> selectedEmails, string subject, string body, string emailType)
        {
            SelectedEmails = selectedEmails;
            Subject = subject;
            Body = body;
            EmailType = emailType;
        }
    }
}
