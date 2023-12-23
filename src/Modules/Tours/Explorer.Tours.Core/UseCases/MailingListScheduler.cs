using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Public;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Explorer.Tours.Core.UseCases;

public class MailingListScheduler : BackgroundService, IMailingListScheduler
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<MailingListScheduler> _logger;

    public MailingListScheduler(IEmailSender emailSender, ILogger<MailingListScheduler> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    private void SendEmails(TimeOnly timeOfDay, int daysPeriod)
    {
        DateTime now = DateTime.Now;
        DateTime firstOccurence = new DateTime(now.Year, now.Month, now.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second);
        if (now.TimeOfDay > timeOfDay.ToTimeSpan())
        {
            firstOccurence = firstOccurence.AddDays(1); // Move to tomorrow if already past given time today
        }

        TimeSpan timeUntilFirstRun = firstOccurence - now;

        int seconds = daysPeriod * 24 * 60 * 60;
        //Timer _timer = new Timer(sendEmails, emails, timeUntilFirstRun, TimeSpan.FromSeconds(seconds)); 
        Timer _timer = new Timer(sendEmails, daysPeriod, timeUntilFirstRun, TimeSpan.FromSeconds(30)); // za testiranje
    }

    public void sendEmails(object? state)
    {
        int daysPeriod = (int)state;

        _logger.LogInformation("Sending daily digest...");

        List<string> emails;
        if (daysPeriod == 1)
        {
            emails = getEmailsOneDay();
        }
        else if (daysPeriod == 3)
        {
            emails = getEmailsThreeDays();
        }
        else
        {
            emails = getEmailsSevenDays();
        }

        foreach (var email in emails)
        {
            _emailSender.SendEmailAsync(email, "Daily digest", "<p>Djes, ovo bi trebalo da ti stize na minut, uzivaj jedno pola sata poyy <333</p>");
        }

        _logger.LogInformation("Daily digest sent.");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //TimeOnly sendTime = new TimeOnly(12, 0, 0);
        TimeOnly sendTime = TimeOnly.FromDateTime(DateTime.Now.AddSeconds(5)); // za testiranje

        SendEmails(sendTime, 1);
        SendEmails(sendTime, 3);
        SendEmails(sendTime, 7);
    }

    private List<string> getEmailsOneDay()
    {
        // TODO: dobaviti sve koji primaju svaki dan
        List<string> emails = new() { "nikolicveljko01@gmail.com", "nikolicveljko789@gmail.com" };
        return emails;
    }

    private List<string> getEmailsThreeDays()
    {
        // TODO: dobaviti sve koji primaju svaki treci dan
        List<string> emails = new() { "veljosimus@gmail.com" };
        return emails;
    }

    private List<string> getEmailsSevenDays()
    {
        // TODO: dobaviti sve koji primaju svake nedelje
        List<string> emails = new() { "duoperfettoacoustic@gmail.com" };
        return emails;
    }
}
