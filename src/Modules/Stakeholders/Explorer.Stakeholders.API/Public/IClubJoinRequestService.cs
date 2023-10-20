using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IClubJoinRequestService
    {
        Result<ClubJoinRequestSendDto> Send(ClubJoinRequestSendDto request);
        Result Respond(long id, ClubJoinRequestResponseDto response);
        Result Cancel(long id);
        Result<PagedResult<ClubJoinRequestByTouristDto>> GetPagedByTourist(long id, int page, int pageSize);
        Result<PagedResult<ClubJoinRequestByClubDto>> GetPagedByClub(long id, int page, int pageSize);
    }
}
