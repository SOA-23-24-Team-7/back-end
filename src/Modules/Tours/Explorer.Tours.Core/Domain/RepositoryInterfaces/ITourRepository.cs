
namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {

        List<Equipment> GetEquipment(long tourId);
        void AddEquipment(long tourId, long equipmentId);
        void DeleteEquipment(long tourId, long equipmentId);
        IEnumerable<long> GetAuthorsTours(long id);
    }
}
