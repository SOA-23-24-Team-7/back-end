using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubMemberManagementService
{
    Result<ClubMemberKickDto> KickTourist(long membershipId, long userId);
    Result AddMember(long clubId, long touristId);
    void DeleteByClubId(long clubId);
}
