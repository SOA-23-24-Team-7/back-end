using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases;

public class ClubInvitationService : IClubInvitationService
{
    private readonly IMapper _mapper;
    private readonly ICrudRepository<ClubInvitation> _invitationRepository;

    public ClubInvitationService(IMapper mapper, ICrudRepository<ClubInvitation> invitationRepository)
    {
        _mapper = mapper;
        _invitationRepository = invitationRepository;
    }

    public Result<ClubInvitationDto> InviteTourist(ClubInvitationDto invitationDto)
    {
        try
        {
            var invitation = _mapper.Map<ClubInvitation>(invitationDto);
            _invitationRepository.Create(invitation);
            return invitationDto;
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}
