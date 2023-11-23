using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterResponseDto, Encounter.Core.Domain.Encounter>, IEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        public EncounterService(ICrudRepository<Encounter.Core.Domain.Encounter> repository, IEncounterRepository encounterRepository, IMapper mapper) : base(repository, mapper)
        {
            _encounterRepository = encounterRepository;
        }

        public Result<PagedResult<EncounterResponseDto>> GetActive(int page, int pageSize)
        {
            var entities = _encounterRepository.GetActive(page, pageSize);
            return MapToDto<EncounterResponseDto>(entities);
        }

    }
}
