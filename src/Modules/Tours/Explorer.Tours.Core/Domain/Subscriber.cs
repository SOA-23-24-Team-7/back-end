using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Subscriber : Entity
    {
        public long TouristId { get; init; }
        public string? EmailAddress { get; init; }
        public int Frequency { get; set; }
        public DateOnly LastTimeSent { get; init; }

        public Subscriber(long touristId, string emailAddress, int frequency, DateOnly lastTimeSent)
        {
            TouristId = touristId;
            EmailAddress = emailAddress;
            Frequency = frequency;
            LastTimeSent = lastTimeSent;
        }
        public Subscriber()
        {
        }
    }
}
