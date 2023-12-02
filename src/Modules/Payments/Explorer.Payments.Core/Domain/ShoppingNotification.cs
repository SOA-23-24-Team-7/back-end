using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class ShoppingNotification : Entity
    {
        public long TourId { get; init; }
        public string Description { get; set; }
        public long TouristId { get; init; }
        public DateTime Created { get; set; }
        public bool HasSeen { get; private set; } = false;
        public string Header { get; set; }
        public ShoppingNotification(string description,long touristId, long tourId)
        {
            Description = description;
            TouristId = touristId;
            TourId = tourId;
            Created = DateTime.UtcNow;
            Header = "Successfully shopping";
        }
        public void SetSeenStatus()
        {
            HasSeen = true;
        }
    }
}
