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
        Result<ClubJoinRequestDto> Send(ClubJoinRequestDto request);
        Result Respond(long id, ClubJoinRequestResponseDto response);
        Result Cancel(long id);
    }
}
