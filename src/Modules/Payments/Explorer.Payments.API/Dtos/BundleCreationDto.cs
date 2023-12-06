using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class BundleCreationDto
    {
        public string Name { get; set; }
        public long Price { get; set; }
        public List<long> TourIds { get; set; }
    }
}
