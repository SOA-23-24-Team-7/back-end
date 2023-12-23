using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Internal;

namespace Explorer.Encounters.Core.Domain.Services;

public class TourStatisticsService : API.Public.ITourStatisticsService
{
    private IEncounterService _encounterService;
    private IInternalTourExecutionSessionService _tourExecutionSessionService;
    private IInternalTourService _tourService;

    public TourStatisticsService(IEncounterService encounterService, IInternalTourExecutionSessionService tourExecutionSessionService, IInternalTourService tourService)
    {
        _encounterService = encounterService;
        _tourExecutionSessionService = tourExecutionSessionService;
        _tourService = tourService;
    }

    public Dictionary<long, double> GetKeyPointEncounterCompletionPercentage(long tourId)
    {

        var keyPointIds = _tourService.GetKeyPointIds(tourId);
        List<long> keyPointIdsSorted = new();

        foreach (var keyPointid in keyPointIds)
        {
            if (_encounterService.GetByKeyPointId(keyPointid) != null)
            {
                keyPointIdsSorted.Add(keyPointid);
            }
        }

        var touristIds = _tourExecutionSessionService.GetTouristsByTourId(tourId);

        Dictionary<long, double> kpEncounterCompletionNumber = new Dictionary<long, double>();

        if (touristIds.Count > 0)
        {
            foreach (var keyPointId in keyPointIdsSorted)
            {
                foreach (var touristId in touristIds)
                {
                    if (_encounterService.IsEncounterInstanceCompleted(touristId, keyPointId))
                    {
                        if (!kpEncounterCompletionNumber.ContainsKey(keyPointId))
                        {
                            kpEncounterCompletionNumber[keyPointId] = 1;
                        }
                        else
                        {
                            kpEncounterCompletionNumber[keyPointId] += 1;
                        }
                    }
                }

            }

            foreach (var kp in keyPointIdsSorted)
            {
                kpEncounterCompletionNumber[kp] = Math.Round((kpEncounterCompletionNumber[kp] / touristIds.Count) * 100, 2);
            }

            return kpEncounterCompletionNumber;
        }

        return null;
    }
}
