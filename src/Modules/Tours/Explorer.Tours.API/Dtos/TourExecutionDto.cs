using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourExecutionDto
    {
        public long TourId { get; set; }
        public bool IsCampaign { get; set; }
    }
}
