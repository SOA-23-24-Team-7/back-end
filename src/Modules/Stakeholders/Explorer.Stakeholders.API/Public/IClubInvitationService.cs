using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public;

public interface IClubInvitationService
{
    Result<ClubInvitationDto> InviteTourist(ClubInvitationDto invitationDto);
    Result Reject(long clubInvitationId, long userId);
    Result Accept(long clubInvitationId, long userId);
}
