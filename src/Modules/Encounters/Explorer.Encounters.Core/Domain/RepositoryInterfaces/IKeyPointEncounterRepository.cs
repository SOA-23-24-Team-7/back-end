namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces;

public interface IKeyPointEncounterRepository
{
    bool IsEncounterInstanceCompleted(long userId, long keyPointId);
}