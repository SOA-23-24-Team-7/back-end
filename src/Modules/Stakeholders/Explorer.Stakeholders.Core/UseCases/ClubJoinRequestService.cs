using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Public.Administration;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubJoinRequestService : IClubJoinRequestService
    {
        private readonly IMapper _mapper;
        private readonly IClubJoinRequestRepository _requestRepository;
        private readonly IClubInvitationRepository _invitationRepository;
        private readonly IClubMemberManagementService _memberManagementService;
        private readonly IClubRepository _clubRepository;
        public ClubJoinRequestService(IMapper mapper, IClubJoinRequestRepository requestRepository, IClubInvitationRepository invitationRepository, IClubMemberManagementService memberManagementService, IClubRepository clubRepository)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
            _invitationRepository = invitationRepository;
            _memberManagementService = memberManagementService;
            _clubRepository = clubRepository;
        }

        public Result<ClubJoinRequestCreatedDto> Send(ClubJoinRequestSendDto request)
        {
            try
            {
                _clubRepository.Get(request.ClubId);

                var joinRequest = _mapper.Map<ClubJoinRequest>(request);
                joinRequest = _requestRepository.Create(joinRequest);
                return _mapper.Map<ClubJoinRequestCreatedDto>(joinRequest);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result Respond(long id, ClubJoinRequestResponseDto response)
        {
            try
            {
                var request = _requestRepository.Get(r => r.Id == id && r.Status == ClubJoinRequestStatus.Pending);

                request.Respond(response.Accepted);
                _requestRepository.Update(request);

                if (response.Accepted)
                {
                    _invitationRepository.DeleteWaiting(request.ClubId, request.TouristId);
                    _memberManagementService.AddMember(request.ClubId, request.TouristId);
                }

                return Result.Ok().WithSuccess("Club Join Request " + (response.Accepted ? "Accepted" : "Rejected"));
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result Cancel(long id)
        {
            try
            {
                var request = _requestRepository.Get(r => r.Id == id && r.Status == ClubJoinRequestStatus.Pending);

                request.Cancel();
                _requestRepository.Update(request);
                return Result.Ok().WithSuccess("Club Join Request Canceled");
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<ClubJoinRequestByTouristDto>> GetPagedByTourist(long id, int page, int pageSize)
        {
            var requests = _requestRepository.GetPagedByTourist(id, page, pageSize);
            return MapToDto<ClubJoinRequestByTouristDto>(requests);
        }

        public Result<PagedResult<ClubJoinRequestByClubDto>> GetPagedByClub(long id, int page, int pageSize)
        {
            try
            {
                _clubRepository.Get(id);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

            var requests = _requestRepository.GetPagedByClub(id, page, pageSize);
            return MapToDto<ClubJoinRequestByClubDto>(requests);
        }

        private PagedResult<T> MapToDto<T>(PagedResult<ClubJoinRequest> requests)
        {
            var requestsDto = requests.Results.Select(_mapper.Map<T>).ToList();
            return new PagedResult<T>(requestsDto, requests.TotalCount);
        }
    }
}
