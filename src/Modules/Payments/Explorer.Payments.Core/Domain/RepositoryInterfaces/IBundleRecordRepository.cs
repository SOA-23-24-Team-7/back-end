using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain.Bundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface IBundleRecordRepository : ICrudRepository<BundleRecord>
{

}
