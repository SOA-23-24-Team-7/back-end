using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubInvitationService
{
    Result<ClubInvitationCreatedDto> InviteTourist(ClubInvitationWithUsernameDto invitationDto);
    Result Reject(long clubInvitationId, long userId);
    Result Accept(long clubInvitationId, long userId);
    Result<PagedResult<ClubInvitationWithClubAndOwnerName>> GetWaitingInvitations(long touristId);
}
