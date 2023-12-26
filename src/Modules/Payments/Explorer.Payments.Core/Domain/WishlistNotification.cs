using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class WishlistNotification
    {
        public long TourId { get; init; }
        public string Description { get; private set; }
        public long TouristId { get; init; }

        public WishlistNotification(long tourId, string description, long touristId)
        {
            TourId = tourId;
            Description = description;
            TouristId = touristId;
        }


    }
}
