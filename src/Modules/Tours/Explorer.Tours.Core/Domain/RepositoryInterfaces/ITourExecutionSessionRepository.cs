using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourExecutionSessionRepository : ICrudRepository<TourExecutionSession>
    {
        TourExecutionSession Get(long tourId, long touristId);
        TourExecutionSession Add(TourExecutionSession tourExecution);
        TourExecutionSession Abandon(long tourId, long touristId);
        TourExecutionSession UpdateNextKeyPoint(long tourExecutionId, long nextKeyPointId);
        TourExecutionSession CompleteTourExecution(long tourExecutionId);
    }
}
