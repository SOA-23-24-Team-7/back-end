using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IMiscEncounterRepository
    {
        PagedResult<MiscEncounter> GetAll(int page, int pageSize);
        MiscEncounter GetById(long id);
    }
}
