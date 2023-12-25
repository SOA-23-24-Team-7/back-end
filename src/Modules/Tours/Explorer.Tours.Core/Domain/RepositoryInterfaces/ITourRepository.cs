using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Equipment> GetEquipment(long tourId);
        void AddEquipment(long tourId, long equipmentId);
        void DeleteEquipment(long tourId, long equipmentId);
        PagedResult<Tour> GetAll(int page, int pageSize);   //dodato
        Tour GetById(long id);  //dodato
        IEnumerable<long> GetAuthorsTours(long id);
        string GetToursName(long id);
        long GetAuthorsId(long id);
        PagedResult<Tour> GetPublishedTours(int page, int pageSize);
        PagedResult<Tour> GetPopularAdventureTours(int page, int pageSize);
        PagedResult<Tour> GetPopularFamilyTours(int page, int pageSize);
        PagedResult<Tour> GetPopularCruiseTours(int page, int pageSize);
        PagedResult<Tour> GetPopularCulturalTours(int page, int pageSize);
    }
}
