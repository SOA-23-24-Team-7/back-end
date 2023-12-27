using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IClubService
    {
        Result<PagedResult<ClubResponseDto>> GetPaged(int page, int pageSize);
        Result<ClubResponseDto> GetById(int id);
        Result<PagedResult<ClubResponseWithOwnerDto>> GetClubsPaged(int page, int pageSize);
        Result<ClubResponseDto> Create<CreateDto>(CreateDto club);
        Result<ClubResponseDto> CreateClubAndMember(ClubCreateDto club, long ownerId);
        Result<ClubResponseDto> Update<UpdateDto>(UpdateDto club);
        Result<PagedResult<ClubResponseDto>> GetOwnerClubs(long ownerId);
        public Result Delete(long id);
    }
}