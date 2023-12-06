using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class BundleRecord : Entity
    {
        public long TouristId { get; init; }
        public long BundleId { get; init; }
        public double Price { get; set; }
        public DateTime PurchasedDate { get; init; }
        public BundleRecord(long touristId, long bundleId, double price)
        {
            TouristId = touristId;
            BundleId = bundleId;
            Price = price;
            PurchasedDate = DateTime.UtcNow;
        }
    }
}
