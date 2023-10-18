using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubMemberManagementService
{
    Result<ClubMemberKickDto> KickTourist(long membershipId, long userId);
}
