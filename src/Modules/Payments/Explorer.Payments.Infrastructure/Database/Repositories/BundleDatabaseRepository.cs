using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.Bundles;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class BundleDatabaseRepository : CrudDatabaseRepository<Bundle, PaymentsContext>, IBundleRepository
{
    public BundleDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }


}
