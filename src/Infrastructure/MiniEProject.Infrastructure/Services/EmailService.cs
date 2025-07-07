using Microsoft.Extensions.Options;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.Shared.Settings;
using System.Net.Mail;
using System.Net;

namespace MiniEProject.Persistence.Services;

public class EmailService : IEmailService
{
    private EmailSetting _emailSettings { get; }

    public EmailService(IOptions<EmailSetting> options)
    {
        _emailSettings = options.Value;
    }
    public async Task SendEmailAsync(IEnumerable<string> toEmails, string subject, string body)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        foreach (var email in toEmails)
        {
            if (string.IsNullOrWhiteSpace(email))
                continue;

            mail.To.Add(email);
        }

        using var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
        {
            Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
            EnableSsl = true
        };

        await smtp.SendMailAsync(mail);
    }
}
