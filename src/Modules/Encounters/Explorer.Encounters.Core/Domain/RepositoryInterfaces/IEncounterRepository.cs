using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository
    {
        PagedResult<Encounter.Encounter> GetActive(int page, int pageSize);
    }
}
