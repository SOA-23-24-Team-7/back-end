using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubMemberKickService
{
    Result<ClubMemberKickDto> Create(ClubMemberKickDto memberKick);
}
