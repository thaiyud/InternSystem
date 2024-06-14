using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InternSystem.Application.Common.EmailService
{
    public interface IEmailService
    {
        //bool SendEmail(IEnumerable<string> toList, string subject, string body);
        //List<string> GetAvailableEmails();
        Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body);
        List<string> GetAvailableEmails();
    }
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderEmail;
        private readonly ILogger<EmailService> _logger;

        private static readonly List<string> AvailableEmails = new List<string>
        {
            "truongdaivy57@gmail.com",
            "eraishopping57@gmail.com"
        };

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _senderEmail = configuration["EmailSettings:Sender"];
            var password = configuration["EmailSettings:Password"];
            var host = configuration["EmailSettings:Host"];
            var port = int.Parse(configuration["EmailSettings:Port"]);

            _smtpClient = new SmtpClient(host, port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_senderEmail, password)
            };

            _logger = logger;
        }

        public List<string> GetAvailableEmails()
        {
            return AvailableEmails;
        }

        public async Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body)
        {
            try
            {
                foreach (var to in toList)
                {
                    var mailMessage = new MailMessage(_senderEmail, to, subject, body);
                    await _smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email.");
                return false;
            }

            return true;
        }
    }

    //public class EmailService : IEmailService
    //{
    //    private readonly IConfiguration _configuration;

    //    private static List<string> availableEmails = new List<string>
    //    {
    //        "truongdaivy57@gmail.com",
    //        "eraishopping57@gmail.com"
    //    };

    //    public List<string> GetAvailableEmails()
    //    {
    //        return availableEmails;
    //    }

    //    public EmailService(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public bool SendEmail(IEnumerable<string> toList, string subject, string body)
    //    {
    //        try
    //        {
    //            var sender = _configuration["EmailSettings:Sender"];
    //            var password = _configuration["EmailSettings:Password"];
    //            var host = _configuration["EmailSettings:Host"];
    //            var port = int.Parse(_configuration["EmailSettings:Port"]);

    //            using (var client = new SmtpClient(host, port))
    //            {
    //                client.EnableSsl = true;
    //                NetworkCredential credential = new NetworkCredential(sender, password);
    //                client.UseDefaultCredentials = false;
    //                client.Credentials = credential;

    //                foreach (var to in toList)
    //                {
    //                    MailMessage msg = new MailMessage(sender, to, subject, body);
    //                    client.Send(msg);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }

    //        return true;
    //    }
    //}
}
