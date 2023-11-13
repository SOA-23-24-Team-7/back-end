using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourToken: Entity
    {
        public long TourId { get; init; }
        public long TouristId { get; init; }
    }
}
