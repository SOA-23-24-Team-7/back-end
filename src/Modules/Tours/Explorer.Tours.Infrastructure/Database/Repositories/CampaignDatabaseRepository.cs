using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class CampaignDatabaseRepository : ICampaignRepository
    {
        private readonly ToursContext _dbContext;

        public CampaignDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Campaign GetById(long Id)
        {
            return _dbContext.Campaigns.FirstOrDefault(c => c.Id == Id);
        }
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<Campaign> GetByTouristId(long touristId)
        {
            return _dbContext.Campaigns.Where(c => c.TouristId == touristId).ToList();
        }
        public void Save(Campaign campaign)
        {
            _dbContext.Campaigns.Add(campaign);
            _dbContext.SaveChanges();
        }

        public Equipment GetEquipment(long id)
        {
            return _dbContext.Equipment.FirstOrDefault(e => e.Id == id);
        }
    }
}
