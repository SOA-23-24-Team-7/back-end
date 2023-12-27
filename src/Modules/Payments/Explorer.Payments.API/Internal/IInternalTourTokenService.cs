using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Internal
{
    public interface IInternalTourTokenService
    {
        long GetTourTokenCount(long tourId);
        public List<long> GetPurchasedToursIds(long touristId);
    }
}
