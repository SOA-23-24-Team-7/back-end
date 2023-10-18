using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
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
    private readonly IUserRepository _userRepository;
    private readonly IClubInvitationRepository _invitationRepository;
    private readonly IClubMembershipRepository _membershipRepository;
    private readonly ICrudRepository<Club> _clubRepository;

    public ClubInvitationService(IMapper mapper, IUserRepository userRepository, IClubInvitationRepository invitationRepository, ICrudRepository<Club> clubRepository, IClubMembershipRepository membershipRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _invitationRepository = invitationRepository;
        _clubRepository = clubRepository;
        _membershipRepository = membershipRepository;
    }

    public Result<ClubInvitationDto> InviteTourist(ClubInvitationDto invitationDto)
    {
        try
        {
            var invitation = _mapper.Map<ClubInvitation>(invitationDto);
            var tourist = _userRepository.GetPersonId(invitationDto.TouristId);
            var club = _clubRepository.Get(invitationDto.ClubId);

            _invitationRepository.Create(invitation);

            return invitationDto;
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result Reject(long clubInvitationId)
    {
        try
        {
            var invitation = _invitationRepository.Get(clubInvitationId);
            invitation.Status = InvitationStatus.Declined;

            _invitationRepository.Update(invitation);

            return Result.Ok().WithSuccess("Club invitation rejected successfully.");
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }

    public Result Accept(long clubInvitationId)
    {
        try
        {
            var invitation = _invitationRepository.Get(clubInvitationId);
            invitation.Status = InvitationStatus.Accepted;

            _invitationRepository.Update(invitation);
            _membershipRepository.Create(new ClubMembership(invitation.ClubId, invitation.TouristId));

            return Result.Ok().WithSuccess("Club invitation rejected successfully.");
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }
}
