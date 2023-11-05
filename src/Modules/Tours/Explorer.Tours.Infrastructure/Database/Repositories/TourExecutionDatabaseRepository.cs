using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourExecutionDatabaseRepository : ITourExecutionRepository
    {
        private readonly ToursContext _dbContext;
        public TourExecutionDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TourExecution Add(TourExecution tourExecution)
        {
            _dbContext.TourExecutions.Add(tourExecution);
            _dbContext.SaveChanges();
            return tourExecution;
        }
        public TourExecution Abandon(long tourId, long touristId)
        {
            //todo: provera da li postoji tour execution
            TourExecution execution = _dbContext.TourExecutions.FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);
            execution.Abandon();
            _dbContext.SaveChanges();
            return execution;
        }

        public TourExecution UpdateNextKeyPoint(long tourExecutionId, long nextKeyPointId)
        {
            //todo: provera da li postoji tour execution
            TourExecution execution = _dbContext.TourExecutions.FirstOrDefault(t => t.Id == tourExecutionId);
            execution.SetNextKeyPointId(nextKeyPointId);
            _dbContext.SaveChanges();
            return execution;
        }
        public TourExecution CompleteTourExecution(long tourExecutionId)
        {
            TourExecution execution = _dbContext.TourExecutions.FirstOrDefault(t => t.Id == tourExecutionId);
            execution.Complete();
            _dbContext.SaveChanges();
            return execution;
        }
        public TourExecution Get(long tourId, long touristId)
        {
            //todo: provera da li postoji tour execution
            TourExecution execution = _dbContext.TourExecutions.FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);
            return execution;
        }
    }
}
