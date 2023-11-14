using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class ClubInvitationService : IClubInvitationService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IClubInvitationRepository _invitationRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IClubJoinRequestRepository _requestRepository;
    private readonly IClubMembershipRepository _membershipRepository;

    private readonly IClubMemberManagementService _clubMemberManagementService;

    public ClubInvitationService(IMapper mapper, IUserRepository userRepository, IClubInvitationRepository invitationRepository, IClubRepository clubRepository, IClubJoinRequestRepository requestRepository, IClubMembershipRepository membershipRepository, IClubMemberManagementService clubMemberManagementService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _invitationRepository = invitationRepository;
        _clubRepository = clubRepository;
        _requestRepository = requestRepository;
        _membershipRepository = membershipRepository;
        _clubMemberManagementService = clubMemberManagementService;
    }

    public Result<ClubInvitationCreatedDto> InviteTourist(ClubInvitationWithUsernameDto invitationDto)
    {
        try
        {
            var user = _userRepository.GetActiveByName(invitationDto.Username);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var dto = new ClubInvitationDto() { ClubId = invitationDto.ClubId, TouristId = user.Id };
            return InviteByTouristId(dto);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }

    private Result<ClubInvitationCreatedDto> InviteByTouristId(ClubInvitationDto invitationDto)
    {
        try
        {
            var invitation = _mapper.Map<ClubInvitation>(invitationDto);
            var club = _clubRepository.Get(invitationDto.ClubId);

            if (isMember(invitationDto.TouristId, invitation.ClubId) || isInvited(invitationDto.TouristId) || isOwner(invitation.TouristId, invitation.ClubId))
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
            }

            var createdInvitaion = _invitationRepository.Create(invitation);
            
            var createdInvitationDto = _mapper.Map<ClubInvitationCreatedDto>(createdInvitaion);

            return createdInvitationDto;
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

    public Result Reject(long clubInvitationId, long userId)
    {
        try
        {
            var invitation = _invitationRepository.Get(clubInvitationId);

            if (!isWaiting(invitation))
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
            }

            if (userId != invitation.TouristId)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
            }

            invitation.Status = InvitationStatus.Declined;

            _invitationRepository.Update(invitation);

            return Result.Ok().WithSuccess("Club invitation rejected successfully.");
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }

    public Result Accept(long clubInvitationId, long userId)
    {
        try
        {
            var invitation = _invitationRepository.Get(clubInvitationId);

            if (!isWaiting(invitation))
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
            }

            if (userId != invitation.TouristId)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
            }

            invitation.Status = InvitationStatus.Accepted;

            _invitationRepository.Update(invitation);
            _clubMemberManagementService.AddMember(invitation.ClubId, userId);
            _requestRepository.DeletePending(invitation.ClubId, invitation.TouristId);

            return Result.Ok().WithSuccess("Club invitation rejected successfully.");
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }

    private bool isWaiting(ClubInvitation clubInvitation)
    {
        return clubInvitation.Status == InvitationStatus.Waiting;
    }

    private bool isMember(long touristId, long clubId)
    {
        var membership = _membershipRepository.GetAll(m => m.ClubId == clubId && m.TouristId == touristId).FirstOrDefault();
        return membership != null;
    }

    private bool isInvited(long touristId)
    {
        var invitation = _invitationRepository.GetAll(i => i.TouristId == touristId && i.Status == InvitationStatus.Waiting).FirstOrDefault();
        return invitation != null;
    }

    private bool isOwner(long touristId, long clubId)
    {
        var club = _clubRepository.Get(clubId);
        return club.OwnerId == touristId;
    }

    public Result<PagedResult<ClubInvitationWithClubAndOwnerName>> GetWaitingInvitations(long touristId)
    {
        var invitations = _invitationRepository.GetAll(i => i.TouristId == touristId && i.Status == InvitationStatus.Waiting);
        var dtos = new List<ClubInvitationWithClubAndOwnerName>();
        foreach (var invitation in invitations)
        {
            dtos.Add(_mapper.Map<ClubInvitationWithClubAndOwnerName>(invitation));
        }
        var result = new PagedResult<ClubInvitationWithClubAndOwnerName>(dtos, dtos.Count);
        return result;
    }
}
