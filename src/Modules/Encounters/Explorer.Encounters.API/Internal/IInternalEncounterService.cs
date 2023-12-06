namespace Explorer.Encounters.API.Internal;

public interface IInternalEncounterService
{
    bool IsEncounterInstanceCompleted(long userId, long keyPointId);
}