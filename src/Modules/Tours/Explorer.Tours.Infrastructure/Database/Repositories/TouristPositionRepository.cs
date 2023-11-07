using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class TouristPositionRepository : ITouristPositionRepository
{
    protected readonly ToursContext DbContext;
    private readonly DbSet<TouristPosition> _dbSet;

    public TouristPositionRepository(ToursContext dbContext)
    {
        DbContext = dbContext;
        _dbSet = DbContext.Set<TouristPosition>();
    }

    public TouristPosition GetByTouristId(long touristId)
    {
        return _dbSet.Where(x => x.TouristId == touristId).First();
    }
}
