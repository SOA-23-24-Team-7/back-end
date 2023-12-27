using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Explorer.Tours.Core.UseCases;

public class MailingListScheduler : BackgroundService, IMailingListScheduler
{

    private readonly IServiceProvider _serviceProvider;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<MailingListScheduler> _logger;

    public MailingListScheduler(IEmailSender emailSender, ILogger<MailingListScheduler> logger, IServiceProvider serviceProvider)
    {
        _emailSender = emailSender;
        _logger = logger;
        _serviceProvider = serviceProvider;
        
    }

    private void SendEmails(TimeOnly timeOfDay, int daysPeriod)
    {
        /*
        DateTime now = DateTime.Now;
        DateTime firstOccurence = new DateTime(now.Year, now.Month, now.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second);
        if (now.TimeOfDay > timeOfDay.ToTimeSpan())
        {
            firstOccurence = firstOccurence.AddDays(1); // Move to tomorrow if already past given time today
        }

        TimeSpan timeUntilFirstRun = firstOccurence - now;

        int seconds = daysPeriod * 24 * 60 * 60;
        //Timer _timer = new Timer(sendEmails, daysPeriod, timeUntilFirstRun, TimeSpan.FromSeconds(seconds));
        Timer _timer = new Timer(sendEmails, daysPeriod, timeUntilFirstRun, TimeSpan.FromSeconds(20)); // za testiranje
        */
    }

    public void sendEmails(object? state)
    {
        /*
        int daysPeriod = (int)state;

        _logger.LogInformation("Sending daily digest...");

        List<string> emails = getEmailsOneDay();
        
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
        */
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            int dayOfSending = 0;
            var timer = new System.Threading.Timer(async _ =>
            {
                // Do your work here
                using (var scope = _serviceProvider.CreateScope())
                {
                    var subService = scope.ServiceProvider.GetRequiredService<ISubscriberService>();
                    var toursRecommendersService = scope.ServiceProvider.GetRequiredService<IToursRecommendersService>();
                    List<SubscriberResponseDto> subs = subService.GetAll();
                    subs = subs.Where(s => dayOfSending % s.Frequency == 0).ToList();
                    foreach (SubscriberResponseDto sub in subs)
                    {
                        string body = File.ReadAllText("../Resources/templateMail.html");

                        string headers = "<tr><th>Tour Name</th><th>Description</th><th>Price</th><th>Distance</th><th>Difficulty</th>";

                        string tables = "";
                        string toursList = "";
                        var tours = toursRecommendersService.GetRecommendedToursForMail(sub.TouristId).Take(5).ToList();
                        var activeTours = toursRecommendersService.GetActiveToursList(sub.TouristId).Take(5).ToList();

                        toursList += "<table><thead>";
                        toursList += headers;
                        foreach (TourResponseDto t in tours)
                        {
                            toursList += "<tr>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Name + "</td>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Description + "</td>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Price + "</td>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Distance + "</td>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Difficulty + "</td>";
                            toursList += "</tr>";
                        }
                        toursList += "</thead></table>";

                        body = body.Replace("[[TABLE1]]", toursList);

                        tables += toursList;

                        toursList = "";
                        toursList += "<table><thead>";
                        toursList += headers;
                        foreach (TourResponseDto t in activeTours)
                        {
                            toursList += "<tr>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Name + "</td>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Description + "</td>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Price + "</td>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Distance + "</td>";
                            toursList += "<td style='border:1px solid #dddddd;text-align:left;padding:8px;'>" + t.Difficulty + "</td>";
                            toursList += "</tr>";
                        }
                        toursList += "</thead></table>";

                        body = body.Replace("[[TABLE2]]", toursList);

                        await _emailSender.SendEmailAsync(sub.EmailAddress, "Daily digest", body );
                    }

                    // Use the dbContext here
                    //TimeOnly sendTime = new TimeOnly(12, 0, 0);

                    //SendEmails(sendTime, 1);
                    //SendEmails(sendTime, 3);
                    //SendEmails(sendTime, 7);
                    dayOfSending++;
                    if( dayOfSending == 22)
                    {
                        dayOfSending = 1;
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(24*3600));

            await Task.Delay(-1, stoppingToken);

         

        }

    }

    private List<string> getEmailsOneDay()
    {
        // TODO: dobaviti sve koji primaju svaki dan
        List<string> emails = new() { "nikola3444@gmail.com" };
        return emails;
    }

    private List<string> getEmailsThreeDays()
    {
        // TODO: dobaviti sve koji primaju svaki treci dan
        List<string> emails = new() { "kulusicbosko@gmail.com" };
        return emails;
    }

    private List<string> getEmailsSevenDays()
    {
        // TODO: dobaviti sve koji primaju svake nedelje
        List<string> emails = new() { "boskokulusic97@gmail.com" };
        return emails;
    }
}
