namespace Explorer.Encounters.Core.Domain.Encounter;

public class KeyPointEncounter : Encounter
{
    public long KeyPointId { get; init; }

    public KeyPointEncounter(string title, string description, double longitude, double latitude, int xpReward, EncounterStatus status, long keyPointId) : base(title, description, longitude, latitude, xpReward, status)
    {
        KeyPointId = keyPointId;
    }
}