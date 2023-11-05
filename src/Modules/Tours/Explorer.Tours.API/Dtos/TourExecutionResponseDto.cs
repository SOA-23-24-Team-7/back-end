using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourExecutionResponseDto
    {
        public long Id { get; set; }
        public TourExecutionStatus Status { get; set; }
        public long TourId { get; set; }
        public long TouristId { get; set; }
        public long NextKeyPointId { get; set; }
        public DateTime LastActivity { get; set; }
    }
    public enum TourExecutionStatus
    {
        Started,
        Abandoned,
        Completed
    }
}
