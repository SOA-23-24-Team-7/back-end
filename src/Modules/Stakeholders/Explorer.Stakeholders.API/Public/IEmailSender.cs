namespace Explorer.Stakeholders.API.Public;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}
