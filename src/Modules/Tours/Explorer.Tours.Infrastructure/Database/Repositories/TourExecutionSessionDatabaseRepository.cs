﻿using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourExecutionSessionDatabaseRepository : ITourExecutionSessionRepository
    {
        private readonly ToursContext _dbContext;
        public TourExecutionSessionDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TourExecutionSession Add(TourExecutionSession tourExecution)
        {
            _dbContext.TourExecutionSessions.Add(tourExecution);
            _dbContext.SaveChanges();
            return tourExecution;
        }
        public TourExecutionSession Abandon(long tourId, long touristId)
        {
            //todo: provera da li postoji tour execution
            TourExecutionSession execution = _dbContext.TourExecutionSessions.FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);
            execution.Abandon();
            _dbContext.SaveChanges();
            return execution;
        }

        public TourExecutionSession UpdateNextKeyPoint(long tourExecutionId, long nextKeyPointId)
        {
            //todo: provera da li postoji tour execution
            TourExecutionSession execution = _dbContext.TourExecutionSessions.FirstOrDefault(t => t.Id == tourExecutionId);
            execution.SetNextKeyPointId(nextKeyPointId);
            _dbContext.SaveChanges();
            return execution;
        }
        public TourExecutionSession CompleteTourExecution(long tourExecutionId)
        {
            TourExecutionSession execution = _dbContext.TourExecutionSessions.FirstOrDefault(t => t.Id == tourExecutionId);
            execution.Complete();
            _dbContext.SaveChanges();
            return execution;
        }
        public TourExecutionSession Get(long tourId, long touristId)
        {
            //todo: provera da li postoji tour execution
            TourExecutionSession execution = _dbContext.TourExecutionSessions.FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);
            return execution;
        }
        public List<TourExecutionSession> GetForTourist(long touristId)
        {
            return _dbContext.TourExecutionSessions.Where(te => te.TouristId == touristId).ToList();
        }
        public TourExecutionSession? GetLive(long touristId)
        {
            return _dbContext.TourExecutionSessions.Where(te => te.TouristId == touristId && te.Status == TourExecutionSessionStatus.Started).FirstOrDefault();
        }
    }
}
