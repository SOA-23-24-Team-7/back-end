using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ICampaignRepository
    {
        public Campaign GetById(long Id);
        public List<Campaign> GetByTouristId(long touristId);
        public void Save (Campaign campaign);
        public void Delete (long id);

        public Equipment GetEquipment(long id);
    }
}
