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
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.UseCases
{
    public class ClubService : CrudService<ClubResponseDto, Club>, IClubService
    {
        private readonly IClubRepository _clubRepository;
        public ClubService(ICrudRepository<Club> crudRepository, IClubRepository clubRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _clubRepository = clubRepository;
        }
        public Result<PagedResult<ClubResponseWithOwnerDto>> GetClubsPaged(int page, int pageSize)
        {
            var result = _clubRepository.GetClubsPaged(page, pageSize);
            return MapToDto<ClubResponseWithOwnerDto>(result);
        }

        public Result<PagedResult<ClubResponseDto>> GetOwnerClubs(long ownerId)
        {
            PagedResult<ClubResponseDto> ownerClubs = new PagedResult<ClubResponseDto>(new List<ClubResponseDto>(), 0);
            var result = CrudRepository.GetPaged(0, 0);
            foreach(var club in result.Results)
            {
                if(club.OwnerId == ownerId)
                    ownerClubs.Results.Add(MapToDto<ClubResponseDto>(club));
            }
            return ownerClubs;
        }
    }
}
