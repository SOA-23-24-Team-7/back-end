using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Public;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        public ClubService(ICrudRepository<Club> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
        public Result<PagedResult<ClubDto>> GetOwnerClubs(long ownerId)
        {
            PagedResult<ClubDto> ownerClubs = new PagedResult<ClubDto>(new List<ClubDto>(), 0);
            var result = CrudRepository.GetPaged(0, 0);
            foreach(var club in result.Results)
            {
                if(club.OwnerId == ownerId)
                    ownerClubs.Results.Add(MapToDto(club));
            }
            return ownerClubs;
        }
    }
}
