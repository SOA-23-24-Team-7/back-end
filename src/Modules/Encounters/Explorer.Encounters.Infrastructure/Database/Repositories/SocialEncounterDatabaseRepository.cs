using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database.Repositories;

public class SocialEncounterDatabaseRepository : ISocialEncounterRepository
{
    private readonly EncountersContext _dbContext;
    private readonly DbSet<SocialEncounter> _dbSet;

    public SocialEncounterDatabaseRepository(EncountersContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<SocialEncounter>();
    }

    public PagedResult<SocialEncounter> GetAll(int page, int pageSize)
    {
        var task = _dbSet.GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

    public SocialEncounter GetById(long id)
    {
        var entity = _dbSet.First(x => x.Id == id);
        if (entity == null) throw new KeyNotFoundException("Not found: " + id);
        return entity;
    }
}
