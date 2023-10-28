using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IPreferenceRepository
    {
        Preference Create(Preference tourPreference);
        Preference GetByUserId(int userId);
        void Delete(long id);
        Preference Update(Preference tourPreference);
    }
}
