using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ICouponRepository
    {
        public Coupon FindByCode(string code);
        public PagedResult<Coupon> GetPagedByAuthorId(int page, int pageSize, long id);
    }
}
