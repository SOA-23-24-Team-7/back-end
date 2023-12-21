using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourRepository : ITourRepository
    {
        protected readonly ToursContext DbContext;
        private readonly DbSet<Tour> _dbSet;

        public TourRepository(ToursContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = DbContext.Set<Tour>();
        }

        public List<Equipment> GetEquipment(long tourId)
        {
            ///SAMO JE OVO URADJENO TREBA OSTATAK TESTIRATI
            var equipmentList = DbContext.Equipment
                                .Where(e => e.Tours.Any(t => t.Id == tourId))
                                .ToList();
            return equipmentList;
        }

        public void AddEquipment(long tourId, long equipmentId)
        {
            var tour = DbContext.Tours
                                .Include(t => t.EquipmentList)
                                .Single(t => t.Id == tourId);
            var equipment = DbContext.Equipment
                                     .Single(e => e.Id == equipmentId);
            tour.EquipmentList.Add(equipment);

            DbContext.SaveChanges();
        }

        public void DeleteEquipment(long tourId, long equipmentId)
        {
            var tour = DbContext.Tours
                                .Include(t => t.EquipmentList)
                                .Single(t => t.Id == tourId);
            var equipment = DbContext.Equipment
                                     .Single(e => e.Id == equipmentId);
            tour.EquipmentList.Remove(equipment);

            DbContext.SaveChanges();
        }

        //Metoda vraca listu ID-jeva tura od poslatog autora
        public IEnumerable<long> GetAuthorsTours(long id)
        {
            return _dbSet.Where(t => t.AuthorId == id).Select(x => x.Id);
        }

        public string GetToursName(long id)
        {
            return _dbSet.FirstOrDefault(t => t.Id == id).Name;
        }

        public long GetAuthorsId(long id)
        {
            return _dbSet.FirstOrDefault(t => t.Id == id).AuthorId;
        }

        public PagedResult<Tour> GetAll(int page, int pageSize)
        {
            var task = _dbSet.Include(x => x.KeyPoints).Include(x => x.Reviews).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Tour GetById(long id)
        {
            var entity = _dbSet.Include(x => x.KeyPoints).Include(x => x.EquipmentList).Include(x => x.Reviews).FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }

        public PagedResult<Tour> GetPublishedTours(int page, int pageSize)
        {
            var task = _dbSet.Include(x => x.KeyPoints).Where(s => s.Status == TourStatus.Published).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Tour> GetPopularAdventureTours(int page, int pageSize)
        {
            var task = _dbSet.Include(x => x.KeyPoints).Where(s => s.Status == TourStatus.Published && s.Category==TourCategory.Adventure).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Tour> GetPopularFamilyTours(int page, int pageSize)
        {
            var task = _dbSet.Include(x => x.KeyPoints).Where(s => s.Status == TourStatus.Published && s.Category == TourCategory.FamilyTrips).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Tour> GetPopularCruiseTours(int page, int pageSize)
        {
            var task = _dbSet.Include(x => x.KeyPoints).Where(s => s.Status == TourStatus.Published && s.Category == TourCategory.Cruise).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Tour> GetPopularCulturalTours(int page, int pageSize)
        {
            var task = _dbSet.Include(x => x.KeyPoints).Where(s => s.Status == TourStatus.Published && s.Category == TourCategory.Cultural).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
