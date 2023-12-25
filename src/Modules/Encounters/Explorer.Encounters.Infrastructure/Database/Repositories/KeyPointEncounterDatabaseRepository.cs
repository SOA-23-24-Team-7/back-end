using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using EncounterInstanceStatus = Explorer.Encounters.Core.Domain.Encounter.EncounterInstanceStatus;

namespace Explorer.Encounters.Infrastructure.Database.Repositories;

public class KeyPointEncounterDatabaseRepository : IKeyPointEncounterRepository
{
    private readonly EncountersContext _dbContext;
    private readonly DbSet<KeyPointEncounter> _dbSet;

    public KeyPointEncounterDatabaseRepository(EncountersContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<KeyPointEncounter>();
    }

    public bool IsEncounterInstanceCompleted(long userId, long keyPointId)
    {
        var encounter = _dbSet.FirstOrDefault(x => x.KeyPointId == keyPointId);
        if (encounter == null) throw new ArgumentException("Encounter doesn't exist");

        var encounterInstance = encounter.Instances.FirstOrDefault(x => x.UserId == userId);
        if (encounterInstance == null) throw new ArgumentException("Encounter instance doesn't exist");

        return encounterInstance.Status == EncounterInstanceStatus.Completed;

    }

    public KeyPointEncounter GetByKeyPoint(long keyPointId)
    {
        var encounter = _dbSet.FirstOrDefault(x => x.KeyPointId == keyPointId);
        if (encounter == null) throw new ArgumentException("Encounter doesn't exist");

        return encounter;
    }

    public KeyPointEncounter GetByKeyPointId(long keyPointId)
    {
        var encounter = _dbSet.FirstOrDefault(x => x.KeyPointId == keyPointId);
        
        return encounter;
    }
}