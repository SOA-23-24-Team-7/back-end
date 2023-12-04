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
        TourExecutionSession GetStarted(long tourId, bool isCampaign, long touristId);
        TourExecutionSession Add(TourExecutionSession tourExecution);
        TourExecutionSession Abandon(long tourExecutionId);
        TourExecutionSession UpdateNextKeyPoint(long tourExecutionId, long nextKeyPointId);
        TourExecutionSession CompleteTourExecution(long tourExecutionId);
        List<TourExecutionSession> GetForTourist(long touristId);
        TourExecutionSession? GetLive(long touristId);
    }
}
