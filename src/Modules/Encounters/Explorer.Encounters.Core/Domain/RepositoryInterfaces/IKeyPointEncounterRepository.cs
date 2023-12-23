using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain.Encounter;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces;

public interface IKeyPointEncounterRepository
{
    bool IsEncounterInstanceCompleted(long userId, long keyPointId);
    KeyPointEncounter GetByKeyPoint(long keyPointId);
    KeyPointEncounter GetByKeyPointId(long keyPointId);

}