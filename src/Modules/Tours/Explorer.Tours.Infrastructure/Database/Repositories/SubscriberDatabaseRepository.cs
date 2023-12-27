using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class SubscriberDatabaseRepository : CrudDatabaseRepository<Subscriber, ToursContext>, ISubscriberRepository
{
    public SubscriberDatabaseRepository(ToursContext dbContext) : base(dbContext) { }

}
