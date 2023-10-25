using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IClubService
    {
        Result<PagedResult<ClubResponseDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<ClubResponseWithOwnerDto>> GetClubsPaged(int page, int pageSize);
        Result<ClubResponseDto> Create<CreateDto>(CreateDto club);
        Result<ClubResponseDto> Update<UpdateDto>(UpdateDto club);
        Result<PagedResult<ClubResponseDto>> GetOwnerClubs(long ownerId);
        public Result Delete(long id);
    }
}
