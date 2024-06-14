using InternSystem.Application.Common.EmailService;
using InternSystem.Application.Features.Interview.Commands;
using MediatR;

namespace InternSystem.Application.Features.Interview.Handlers
{
    public class SendEmailsCommandHandler : IRequestHandler<SendEmailsCommand, bool>
    {
        private readonly IEmailService _emailService;

        public SendEmailsCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<bool> Handle(SendEmailsCommand request, CancellationToken cancellationToken)
        {
            if (!request.SelectedEmails.Any())
            {
                return false;
            }

            string emailBody = request.Body;

            switch (request.EmailType)
            {
                case "Interview Date":
                    emailBody = $"Interview Date Info: {request.Body}";
                    break;
                case "Interview Result":
                    emailBody = $"Interview Result Info: {request.Body}";
                    break;
                case "Internship Time":
                    emailBody = $"Internship Time Info: {request.Body}";
                    break;
                case "Internship Information":
                    emailBody = $"Internship Information: {request.Body}";
                    break;
                default:
                    emailBody = request.Body;
                    break;
            }

            return await _emailService.SendEmailAsync(request.SelectedEmails, request.Subject, emailBody);
        }
    }
}
