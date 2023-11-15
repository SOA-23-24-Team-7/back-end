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
        return _dbSet.Where(x => x.TouristId == touristId).FirstOrDefault();
    }

    public TouristPosition Update(TouristPosition touristPosition)
    {
        var touristPositionOld = GetByTouristId(touristPosition.TouristId);
        touristPositionOld.Longitude = touristPosition.Longitude;
        touristPositionOld.Latitude = touristPosition.Latitude;

        try
        {
            DbContext.Update(touristPositionOld);
            DbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        return touristPositionOld;
    }
}
