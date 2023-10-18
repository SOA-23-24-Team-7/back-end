using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class RatingService : CrudService<RatingDto, Rating>, IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingService(ICrudRepository<Rating> repository, IMapper mapper, IRatingRepository ratingRepository) : base(repository, mapper) 
        {
            _ratingRepository = ratingRepository;
        }

        public Result<RatingDto> GetByUser(int id)
        {
            //var ratings = CrudRepository.GetPaged(2, 2);
            //Rating? r = ratings.Results.Find(r => r.UserId == id);
            var r = _ratingRepository.GetByUserId(id);
            return MapToDto(r);
        }
    }
}
