using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;

namespace Explorer.Encounters.Core.UseCases
{
    public class TouristProgressService : CrudService<TouristProgressResponseDto, TouristProgress>, ITouristProgressService
    {
        private readonly ITouristProgressRepository _touristProgressRepository;
        public TouristProgressService(ICrudRepository<TouristProgress> repository, ITouristProgressRepository touristProgressRepository, IMapper mapper) : base(repository, mapper)
        {
            _touristProgressRepository = touristProgressRepository;
        }
    }
}
