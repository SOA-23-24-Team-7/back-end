using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourExecutionSessionResponseDto
    {
        public long Id { get; set; }
        public TourExecutionSessionStatus Status { get; set; }
        public long TourId { get; set; }
        public long TouristId { get; set; }
        public long NextKeyPointId { get; set; }
        public DateTime LastActivity { get; set; }
    }
    public enum TourExecutionSessionStatus
    {
        Started,
        Abandoned,
        Completed
    }
}
