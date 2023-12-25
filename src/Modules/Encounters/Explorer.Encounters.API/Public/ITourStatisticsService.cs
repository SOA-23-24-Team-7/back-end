namespace Explorer.Encounters.API.Public;

public interface ITourStatisticsService
{
    Dictionary<long, double> GetKeyPointEncounterCompletionPercentage(long tourId);

}
