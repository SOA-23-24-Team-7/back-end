namespace Explorer.Encounters.Core.Domain.Encounter;

public class SocialEncounter : Encounter
{
    public int PeopleNumber { get; init; }
    public SocialEncounter() { }
    public SocialEncounter(string title, string description, double longitude, double latitude, double radius, int xp, EncounterStatus status, int peopleNumber, EncounterType encounterType) : base(title, description, longitude, latitude, radius, xp, status, encounterType)
    {
        PeopleNumber = peopleNumber;
        Validate();
    }

    public void Validate()
    {
        if (PeopleNumber < 1) throw new ArgumentException("Invalid people number");
    }

    public override void CompleteEncounter(long userId)
    {
        var encounterInstance = Instances.FirstOrDefault(x => x.UserId == userId);
        var activatedInstances = Instances.Count(i => i.Status == EncounterInstanceStatus.Active);
        if(activatedInstances >= PeopleNumber)
            encounterInstance.Complete();
        else
            throw new ArgumentException("Not enought users that activated social encounter.");
    }
}
