using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class TouristProgressService : CrudService<TouristProgressResponseDto, TouristProgress>, ITouristProgressService
    {
        private readonly ITouristProgressRepository _touristProgressRepository;
        public TouristProgressService(ICrudRepository<TouristProgress> repository, ITouristProgressRepository touristProgressRepository, IMapper mapper) : base(repository, mapper)
        {
            _touristProgressRepository = touristProgressRepository;
        }

        public Result<TouristProgressResponseDto> GetByUserId(long userId)
        {
            try
            {
                var progress = _touristProgressRepository.GetByUserId(userId);
                return MapToDto<TouristProgressResponseDto>(progress);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }
    }
}
