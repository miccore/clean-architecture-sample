using System.Net.Mail;
using Miccore.CleanArchitecture.Sample.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Miccore.CleanArchitecture.Sample.Infrastructure.Implementations
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly string _client;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
            _client = "localhost";
        }

        /// <summary>
        /// send email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string to, string from, string subject, string body)
        {
            // create email client
            var emailClient = new SmtpClient(_client);
            // create message
            var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body
            };
            // add sender
            message.To.Add(new MailAddress(to));
            // send email
            await emailClient.SendMailAsync(message);
            // log
            _logger.LogWarning("Sending email to {to} from {from} with subject {subject}.", to, from, subject);
        }

    }
}