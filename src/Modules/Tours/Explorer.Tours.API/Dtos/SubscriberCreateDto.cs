using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class SubscriberCreateDto
    {
        public long TouristId { get; set; }
        public string? EmailAddress { get; set; }
        public int Frequency { get; set; }
        public DateOnly LastTimeSent { get; set; }
    }
}
