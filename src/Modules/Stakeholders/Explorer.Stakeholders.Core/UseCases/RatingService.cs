using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class RatingService : CrudService<RatingResponseDto, Rating>, IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingService(ICrudRepository<Rating> repository, IMapper mapper, IRatingRepository ratingRepository) : base(repository, mapper) 
        {
            _ratingRepository = ratingRepository;
        }

        public Result<PagedResult<RatingWithUserDto>> GetRatingsPaged(int page, int pageSize)
        {
            var result = _ratingRepository.GetRatingsPaged(page, pageSize);
            return MapToDto<RatingWithUserDto>(result);
        }
        public Result<RatingResponseDto> GetByUser(long id)
        {
            var r = _ratingRepository.GetByUserId(id);
            return MapToDto<RatingResponseDto>(r);
        }
    }
}
