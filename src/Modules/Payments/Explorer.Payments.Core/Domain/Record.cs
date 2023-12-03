using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain
{
    public class Record : Entity
    {
        public long Id { get; set; }
        public long TouristId { get; init; }
        public long TourId { get; init; }
        public double Price { get; set; }
        public DateTime PurchasedDate { get; init; }
        public Record(long touristId, long tourId, double price)
        {
            TouristId = touristId;
            TourId = tourId;
            Price = price;
            PurchasedDate = DateTime.UtcNow;
        }
    }
}
