using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface ITouristProgressRepository
    {
        TouristProgress GetByUserId(long id);
    }
}
