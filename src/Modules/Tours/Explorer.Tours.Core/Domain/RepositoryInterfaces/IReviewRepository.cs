using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IReviewRepository
    {
        PagedResult<Review> GetPagedByTourId(int page, int pageSize, long tourId);
        bool ReviewExists(long touristId, long tourId);
    }
}
