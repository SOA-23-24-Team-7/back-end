using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class WishlistNotification: Entity
    {
        public long TourId { get; init; }
        public string Description { get; private set; }
        public long TouristId { get; init; }

        public WishlistNotification(long tourId, long touristId, string description)
        {
            TourId = tourId;
            Description = description;
            TouristId = touristId;
        }


    }
}
