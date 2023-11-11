
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
    }
}
