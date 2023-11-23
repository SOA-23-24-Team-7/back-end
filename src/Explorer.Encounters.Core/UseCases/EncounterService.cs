using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterResponseDto, Encounter.Core.Domain.Encounter>, IEncounterService
    {
        public EncounterService(ICrudRepository<Encounter.Core.Domain.Encounter> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
