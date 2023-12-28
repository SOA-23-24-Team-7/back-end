namespace Explorer.Encounters.Core.Domain.Encounter;

public class KeyPointEncounter : Encounter
{
    public long KeyPointId { get; init; }

    public KeyPointEncounter(string title, string description, string picture, double longitude, double latitude, double radius, int xpReward, EncounterStatus status, long keyPointId) : base(title, description, picture, longitude, latitude, radius, xpReward, status, EncounterType.KeyPoint)
    {
        KeyPointId = keyPointId;
    }

    public override void CompleteEncounter(long userId)
    {
        base.CompleteEncounter(userId);
    }
}