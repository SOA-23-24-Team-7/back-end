using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.UseCases
{
    public class ClubService : CrudService<ClubResponseDto, Club>, IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IClubMemberManagementService _clubMemberManagementService;
        public ClubService(ICrudRepository<Club> crudRepository, IClubRepository clubRepository, IMapper mapper, IClubMemberManagementService clubMemberManagementService) : base(crudRepository, mapper)
        {
            _clubRepository = clubRepository;
            _clubMemberManagementService = clubMemberManagementService;
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
            foreach (var club in result.Results)
            {
                if (club.OwnerId == ownerId)
                    ownerClubs.Results.Add(MapToDto<ClubResponseDto>(club));
            }
            return ownerClubs;
        }

        public Result<ClubResponseDto> GetById(int id)
        {
            var result = _clubRepository.Get(c => c.Id == id);
            return MapToDto<ClubResponseDto>(result);
        }

        public Result<ClubResponseDto> CreateClubAndMember(ClubCreateDto club, long ownerId)
        {
            try
            {
                var result = Create(club);
                _clubMemberManagementService.AddMember(result.Value.Id, ownerId);
                return result;
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}