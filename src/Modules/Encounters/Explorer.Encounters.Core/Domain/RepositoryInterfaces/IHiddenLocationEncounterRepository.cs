using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounter;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IHiddenLocationEncounterRepository
    {
        PagedResult<HiddenLocationEncounter> GetAll(int page, int pageSize);
        HiddenLocationEncounter GetHiddenLocationEncounterById(long id);
    }
}
