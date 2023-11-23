using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository
    {
        PagedResult<Explorer.Encounter.Core.Domain.Encounter> GetActive(int page, int pageSize);
    }
}
