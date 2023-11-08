using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
