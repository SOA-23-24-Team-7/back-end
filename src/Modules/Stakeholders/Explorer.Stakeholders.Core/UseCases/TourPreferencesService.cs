using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourPreferencesService : CrudService<TourPreferencesDto, TourPreferences>, ITourPreferencesService
    {
        public TourPreferencesService(ICrudRepository<TourPreferences> repository, IMapper mapper) : base(repository, mapper) { }

    }
}
