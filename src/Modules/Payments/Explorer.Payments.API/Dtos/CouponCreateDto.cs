using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class CouponCreateDto
    {
        public double Discount { get; set; }
        public long TourId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool AllFromAuthor { get; init; }
        public long AuthorId { get; set; }
    }
}
