using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class ClubMemberManagementService : IClubMemberManagementService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IClubMembershipRepository _clubMembershipRepository;


    public ClubMemberManagementService(IMapper mapper, IUserRepository userRepository, IPersonRepository personRepository, IClubRepository clubRepository, IClubMembershipRepository clubMembershipRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _personRepository = personRepository;
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

    public Result<PagedResult<ClubMemberDto>> GetMembers(long clubId)
    {
        try
        {
            var club = _clubRepository.Get(clubId);
            var memberships = _clubMembershipRepository.GetAll(m => m.ClubId == clubId);
            var dtos = new List<ClubMemberDto>();
            foreach (var membership in memberships)
            {
                var person = _personRepository.GetByUserId(membership.TouristId);
                var memberDto = new ClubMemberDto() { UserId = person.UserId, FirstName = person.Name, LastName = person.Surname, Username = membership.Tourist.Username, MembershipId = membership.Id, ProfilePicture = membership.Tourist.ProfilePicture };
                dtos.Add(memberDto);
            }
            var result = new PagedResult<ClubMemberDto>(dtos, dtos.Count);
            return result;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }

    public Result<PagedResult<ClubResponseDto>> GetUserClubs(long userId)
    {
        try
        {
            var dtos = new List<ClubResponseDto>();
            var memberships = _clubMembershipRepository.GetAll(m => m.TouristId == userId);
            
            foreach (var membership in memberships)
            {
                var club = _clubRepository.Get(membership.ClubId);
                var clubDto = new ClubResponseDto() { Id = club.Id, Name = club.Name, Description = club.Description, Image = club.Image, OwnerId = club.OwnerId };
                dtos.Add(clubDto);
            }
            var result = new PagedResult<ClubResponseDto>(dtos, dtos.Count);
            return result;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
        }
    }

    public Result KickTourist(long membershipId, long userId)
    {
        try
        {
            var membership = _clubMembershipRepository.Get(membershipId);
            var club = _clubRepository.Get(membership.ClubId);

            if (club.OwnerId != userId && membership.TouristId != userId)
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
