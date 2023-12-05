using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository
    {
        PagedResult<Encounter.Encounter> GetActive(int page, int pageSize);
        PagedResult<Encounter.Encounter> GetAll(int page, int pageSize);
        Encounter.Encounter GetById(long id);
    }
}
