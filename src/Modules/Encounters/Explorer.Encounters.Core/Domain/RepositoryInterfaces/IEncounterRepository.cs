using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounter;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository
    {
        PagedResult<Encounter.Encounter> GetActive(int page, int pageSize);
        PagedResult<Encounter.Encounter> GetAll(int page, int pageSize);
        PagedResult<Encounter.Encounter> GetAllInRangeOf(double range, double longitude, double latitude, int page, int pageSize);
        PagedResult<Encounter.Encounter> GetAllDoneByUser(int currentUserId, int page, int pageSize);
        Encounter.Encounter GetById(long id);
        EncounterInstance GetInstance(long userId, long encounterId);
    }
}
