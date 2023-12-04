using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class RecordCreateDto
    {
        public long TouristId { get; set; }
        public long TourId { get; set; }
        public double Price { get; set; }
    }
}
