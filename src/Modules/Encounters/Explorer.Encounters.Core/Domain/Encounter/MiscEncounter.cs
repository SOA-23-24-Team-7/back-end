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
        public MiscEncounter(bool challangeDone, string title, string description, double longitude, double latitude, int xpReward, EncounterStatus status) : base(title, description, longitude, latitude, xpReward, status)
        {
            ChallengeDone = challangeDone;
        }


    }
}
