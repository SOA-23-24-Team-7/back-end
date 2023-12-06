using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Bundles;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class BundleRecordDatabaseRepository : CrudDatabaseRepository<BundleRecord, PaymentsContext>, IBundleRecordRepository
    {
        public BundleRecordDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }
    }
}
