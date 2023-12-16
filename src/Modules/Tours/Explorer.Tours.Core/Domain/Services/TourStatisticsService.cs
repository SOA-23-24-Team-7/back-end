using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;

namespace Explorer.Tours.Core.Domain.Services;

public class TourStatisticsService : ITourStatisticsService
{
    private ITourExecutionSessionService _tourExecutionSessionService;
    private IInternalTourService _tourService;
    public TourStatisticsService(ITourExecutionSessionService tourExecutionSessionService, IInternalTourService tourService)
    {
        _tourExecutionSessionService = tourExecutionSessionService;
        _tourService = tourService;
    }

    public int GetNumberOfTourExecutionSessions(long tourId)
    {
        var tourExectutionSessions = _tourExecutionSessionService.GetAll();
        int numberOfSessions = tourExectutionSessions.Value.Count(session => session.TourId == tourId);
        return numberOfSessions;
    }

    public int GetNumberOfCompletedTourExecutionSessions(long tourId)
    {
        var tourExectutionSessions = _tourExecutionSessionService.GetAll();
        int numberOfSessions = tourExectutionSessions.Value.Count(session => session.TourId == tourId && session.Progress == 100.00); //moze i preko statusa da se vidi da li je completed
        return numberOfSessions;
    }

    public int GetNumberOfStartedToursByPurchase(long authorId)
    {
        List<long> authorsTours = _tourService.GetAuthorsTours(authorId).ToList();

        int counter = 0;
        foreach (long tourId in authorsTours)
        {
            List<long> ids = new List<long>();
            var tourSessions = _tourExecutionSessionService.GetByTourId(tourId);
            foreach (var session in tourSessions.Value)
            {
                if (!ids.Contains(session.TouristId))
                {
                    counter++;
                    ids.Add(session.TouristId);
                }
            }

        }
        return counter;
    }

    public int GetNumberOfCompletedToursByPurchase(long authorId)
    {
        List<long> authorsTours = _tourService.GetAuthorsTours(authorId);

        int counter = 0;
        foreach (long tourId in authorsTours)
        {
            List<long> ids = new List<long>();
            var tourSessions = _tourExecutionSessionService.GetByTourId(tourId);
            foreach (var session in tourSessions.Value)
            {
                if (!ids.Contains(session.TouristId) && session.Progress == 100.00)
                {
                    counter++;
                    ids.Add(session.TouristId);
                }
            }

        }
        return counter;
    }

    public List<long> GetMaxProgressDistribution(long authorId)
    {
        List<long> authorsTours = _tourService.GetAuthorsTours(authorId);
        long firstQuarterCompletionCount = 0;
        long secondQuarterCompletionCount = 0;
        long thirdQuarterCompletionCount = 0;
        long fourthQuarterCompletionCount = 0;

        foreach (long tourId in authorsTours)
        {
            List<long> touristIdsInserted = new List<long>();
            var touristIds = _tourExecutionSessionService.GetTouristsIds();

            foreach (long id in touristIds)
            {
                var tourSessions = _tourExecutionSessionService.GetByTourAndTouristId(tourId, id);
                double maxProgress = 0.0;
                if (tourSessions.Value.Count() > 0)
                {
                    foreach (var session in tourSessions.Value)
                    {
                        // Proveravamo da li je turista vise puta pokretao istu turu, ako jeste, uzimamo u obzir pokretanje na kojem je imao veci progres
                        if (!touristIdsInserted.Contains(session.TouristId))
                        {
                            maxProgress = session.Progress;
                            touristIdsInserted.Add(session.TouristId);
                        }
                        else if (session.Progress > maxProgress)
                        {
                            maxProgress = session.Progress;
                        }
                    }

                    // Svrstavamo progres u odredjenu kategoriju
                    if (maxProgress <= 25)
                    {
                        firstQuarterCompletionCount++;
                    }
                    else if (maxProgress <= 50)
                    {
                        secondQuarterCompletionCount++;
                    }
                    else if (maxProgress <= 75)
                    {
                        thirdQuarterCompletionCount++;
                    }
                    else
                    {
                        fourthQuarterCompletionCount++;
                    }
                }
            }

        }
        
        // Formiramo listu koju vracamo na frontu grafikonima radi iscrtavanja
        List<long> ret = new()
        {
            firstQuarterCompletionCount,
            secondQuarterCompletionCount,
            thirdQuarterCompletionCount,
            fourthQuarterCompletionCount
        };

        return ret;
    }


}
