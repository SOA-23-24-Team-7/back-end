using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Encounter;

public class SocialEncounter : Encounter
{
    public int PeopleNumber { get; init; }
    public int Area {  get; init; }
    public SocialEncounter() { }
    public SocialEncounter(string title, string description, double longitude, double latitude, int xp, EncounterStatus status, int peopleNumber, int area) : base(title, description, longitude, latitude, xp, status)
    {
        PeopleNumber = peopleNumber;
        Area = area;
    }
}
