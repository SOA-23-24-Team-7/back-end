using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class RecordResponseDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long TourId { get; set; }
        public double Price { get; set; }
        public DateTime PurchasedDate { get; set; }
        public string? TourName { get; set; }
    }
}
