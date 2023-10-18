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
    private readonly ICrudRepository<Club> _clubRepository;
    private readonly IClubMembershipRepository _clubMembershipRepository;

    public ClubMemberManagementService(IMapper mapper, IUserRepository userRepository, ICrudRepository<Club> clubRepository, IClubMembershipRepository clubMembershipRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _clubRepository = clubRepository;
        _clubMembershipRepository = clubMembershipRepository;
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
