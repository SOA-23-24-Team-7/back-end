using Explorer.Stakeholders.API.Public;
using System.Net;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.UseCases;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var senderEmail = "explorer.team.service@gmail.com";
        var senderName = "Explorer";
        var password = "qpmg yemi mldm soxz";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(senderEmail, password)
        };

        var mailMessage = new MailMessage()
        {
            From = new MailAddress(senderEmail, senderName),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        var result = client.SendMailAsync(mailMessage);

        return result;
    }
}
