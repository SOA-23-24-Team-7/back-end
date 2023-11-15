using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TouristEquipment;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Public;

namespace Explorer.Tours.Core.UseCases
{
    public class TouristEquipmentService : CrudService<TouristEquipmentResponseDto, TouristEquipment>, ITouristEquipmentService
    {
        public TouristEquipmentService(ICrudRepository<TouristEquipment> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
