using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class ClubMemberManagementService : IClubMemberManagementService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IClubMembershipRepository _clubMembershipRepository;

    public ClubMemberManagementService(IMapper mapper, IUserRepository userRepository, IClubRepository clubRepository, IClubMembershipRepository clubMembershipRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _clubRepository = clubRepository;
        _clubMembershipRepository = clubMembershipRepository;
    }

    public Result AddMember(long clubId, long touristId)
    {
        try
        {
            var club = _clubRepository.Get(clubId);

            var membership = new ClubMembership(clubId, touristId);
            _clubMembershipRepository.Create(membership);

            return Result.Ok().WithSuccess("Member added successfully.");
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }

    public void DeleteByClubId(long clubId)
    {
        var memberships = _clubMembershipRepository.GetAll(i => i.ClubId == clubId);
        foreach (var membership in memberships)
        {
            _clubMembershipRepository.Delete(membership.Id);
        }
    }

    public Result<ClubMemberKickDto> KickTourist(long membershipId, long userId)
    {
        try
        {
            var membership = _clubMembershipRepository.Get(membershipId);
            var club = _clubRepository.Get(membership.ClubId);

            if (membership == null)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
            }

            if (club.OwnerId != userId)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
            }

            _clubMembershipRepository.Delete(membershipId);

            return Result.Ok().WithSuccess("Member kicked successfully.");
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }
}
