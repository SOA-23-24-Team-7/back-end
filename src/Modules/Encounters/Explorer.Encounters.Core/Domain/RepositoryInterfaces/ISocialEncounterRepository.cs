using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounter;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces;

public interface ISocialEncounterRepository
{
    PagedResult<SocialEncounter> GetAll(int page, int pageSize);
    SocialEncounter GetById(long id);
}
