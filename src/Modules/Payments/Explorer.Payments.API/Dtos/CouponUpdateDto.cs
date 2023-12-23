using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class CouponUpdateDto
    {
        public long Id { get; set; }
        public double Discount { get; set; }
        public long TourId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool AllFromAuthor { get; set; }
        public long AuthorId { get; set; }
    }
}
