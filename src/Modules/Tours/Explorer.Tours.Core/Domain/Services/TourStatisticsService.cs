using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
        IEnumerable<long> authorsTours = _tourService.GetAuthorsTours(authorId);
        
        int counter = 0;
        foreach(long tourId in authorsTours)
        {
            List<long> ids = new List<long>();
            var tourSessions = _tourExecutionSessionService.GetByTourId(tourId);
            foreach(var session in tourSessions.Value)
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
        IEnumerable<long> authorsTours = _tourService.GetAuthorsTours(authorId);

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
        IEnumerable<long> authorsTours = _tourService.GetAuthorsTours(authorId);
        long firstQuarterCompletionCount = 0;
        long secondQuarterCompletionCount = 0;
        long thirdQuarterCompletionCount = 0;
        long fourthQuarterCompletionCount = 0;
        int counter = 0;

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
                        if (!touristIdsInserted.Contains(session.TouristId))
                        {
                            maxProgress = session.Progress;
                        }
                        else if (session.Progress > maxProgress)
                        {
                            maxProgress = session.Progress;
                        }
                    }

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
                    else if (maxProgress <= 100)
                    {
                        fourthQuarterCompletionCount++;
                    }
                }
            }

        }
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
