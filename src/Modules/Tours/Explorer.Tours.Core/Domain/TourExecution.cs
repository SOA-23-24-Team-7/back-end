using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourExecution : Entity
    {
        public TourExecutionStatus Status { get; private set; }
        public long TourId { get; init; }
        public long TouristId { get; init; }
        public long NextKeyPointId { get; private set; }
        public DateTime LastActivity { get; private set; }
        public TourExecution(long tourId, long touristId, long nextKeyPointId) 
        {
            Status = TourExecutionStatus.Started;
            LastActivity = DateTime.UtcNow;
            TourId = tourId;
            TouristId = touristId;
            NextKeyPointId = nextKeyPointId;
        }
        public void Abandon()
        {
            LastActivity = DateTime.UtcNow;
            Status = TourExecutionStatus.Abandoned;
        }
        public void Complete()
        {
            LastActivity = DateTime.UtcNow;
            Status = TourExecutionStatus.Completed;
            NextKeyPointId = -1;
        }
        public void SetNextKeyPointId(long keyPointId)
        {
            LastActivity = DateTime.UtcNow;
            NextKeyPointId = keyPointId;
        }
    }
}

public enum TourExecutionStatus
{
    Started,
    Abandoned,
    Completed
}