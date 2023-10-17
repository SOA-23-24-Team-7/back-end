using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
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
        public RatingService(ICrudRepository<Rating> repository, IMapper mapper) : base(repository, mapper) {}

        public Result<RatingDto> GetByUser(int id)
        {
            var ratings = CrudRepository.GetPaged(1, 1);
            Rating? r = ratings.Results.Find(r => r.UserId == id);
            
            return MapToDto(r);
        }
    }
}
