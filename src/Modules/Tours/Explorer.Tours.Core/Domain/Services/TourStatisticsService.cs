using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.TourAuthoring;

namespace Explorer.Tours.Core.Domain.Services;

public class TourStatisticsService : ITourStatisticsService
{
    private ITourExecutionSessionService _tourExecutionSessionService;
    private IInternalTourService _tourService;
    private IKeyPointService _keyPointService;

    public TourStatisticsService(ITourExecutionSessionService tourExecutionSessionService, IInternalTourService tourService, IKeyPointService keyPointService)
    {
        _tourExecutionSessionService = tourExecutionSessionService;
        _tourService = tourService;
        _keyPointService = keyPointService;
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
                        if (session.Progress > maxProgress)
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

    public List<double> GetKeyPointVisitPercentage(long tourId)
    {
        var tour  = _tourService.Get(tourId);
        var touristIds = _tourExecutionSessionService.GetTouristsIds();
        List<TourExecutionSessionResponseDto> tourExecutionsSorted = new();

        foreach (var touristId in touristIds)
        {
            if(_tourExecutionSessionService.GetMaximumPorgressExecutionsForTourists(tourId, touristId) != null)
            {
                tourExecutionsSorted.Add(_tourExecutionSessionService.GetMaximumPorgressExecutionsForTourists(tourId, touristId).Value);
            }
        }

        long[] counters = new long[tour.Value.KeyPoints.Count];

        foreach(var session in tourExecutionsSorted)
        {
            if(session.NextKeyPointId == -1 && session.Progress == 100)
            {
                for(int i=0; i < counters.Length; i++)
                {
                    counters[i]++;
                }
            }
            else if(_keyPointService.GetById(session.NextKeyPointId).Value.Order > 0)
            {
                var keyPoint = _keyPointService.GetById(session.NextKeyPointId);

                for(long i = (keyPoint.Value.Order - 1); i >= 0; i--)
                {
                    counters[i]++;
                }
            }
        }

        if(tourExecutionsSorted.Count > 0)
        {

            List<double> ret = new();

            for (int i = 0; i < counters.Length; i++)
            {
                double value = Math.Round((counters[i] / (double)tourExecutionsSorted.Count) * 100, 2);
                ret.Add(value);
            }

            return ret;
        }

        List<double> retIfNull = new();

        for (int i = 0; i < counters.Length; i++)
        {
            retIfNull.Add(0);
        }

        return retIfNull;
    }

}
