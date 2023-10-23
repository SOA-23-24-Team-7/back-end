using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubInvitationService
{
    Result<ClubInvitationDto> InviteTourist(ClubInvitationDto invitationDto);
    Result Reject(long clubInvitationId, long userId);
    Result Accept(long clubInvitationId, long userId);
}
