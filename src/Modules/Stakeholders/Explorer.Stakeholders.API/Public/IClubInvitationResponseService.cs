using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubInvitationResponseService
{
    Result<ClubInvitationResponseDto> Create(ClubInvitationResponseDto response);
}
