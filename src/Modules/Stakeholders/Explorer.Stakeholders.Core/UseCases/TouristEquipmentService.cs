using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.TouristEquipment;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TouristEquipmentService : CrudService<TouristEquipmentResponseDto, TouristEquipment>, ITouristEquipmentService
    {
        public TouristEquipmentService(ICrudRepository<TouristEquipment> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
