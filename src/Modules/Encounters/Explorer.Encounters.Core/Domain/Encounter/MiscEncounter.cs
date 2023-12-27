using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Explorer.Encounters.Core.Domain.Encounter
{
    public class MiscEncounter : Encounter
    {
        public bool ChallengeDone { get; init; }

        public MiscEncounter(bool challengeDone, string title, string description, string picture, double longitude, double latitude, double radius, int xpReward, EncounterStatus status, EncounterType type)
     : base(title, description, picture, longitude, latitude, radius, xpReward, status, type)
        {
            ChallengeDone = challengeDone;
        }


    }
}
