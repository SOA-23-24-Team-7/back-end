namespace Explorer.Tours.API.Internal;

public interface IInternalKeyPointService
{
    bool IsToursAuthor(long userId, long id);
    double GetKeyPointLongitude(long keyPointId);
    double GetKeyPointLatitude(long keyPointId);
    void AddEncounter(long keyPointId, bool isRequired);
    bool CheckEncounterExists(long keyPointId);
}