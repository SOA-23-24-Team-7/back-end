using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourExecutionInfoDto
    {
        //Tour info
        public string Name { get; set; }
        public int Difficulty { get; init; }
        public List<string> Tags { get; init; }
        public TourStatus TourStatus { get; private set; }
        public double Distance { get; init; }


        //Tour execution info
        public DateTime LastActivity { get; private set; }
        public TourExecutionSessionStatus TourExecutionStatus { get; private set; }
    }
}
