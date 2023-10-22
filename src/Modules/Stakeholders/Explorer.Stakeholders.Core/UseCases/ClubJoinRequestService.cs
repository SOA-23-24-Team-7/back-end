using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using AutoMapper;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubJoinRequestService : IClubJoinRequestService
    {
        private readonly IMapper _mapper;
        private readonly IClubJoinRequestRepository _requestRepository;
        private readonly IClubInvitationService _clubInvitationService;
        private readonly IClubMemberManagementService _clubMemberManagementService;
        public ClubJoinRequestService(IMapper mapper, IClubJoinRequestRepository requestRepository, IClubInvitationService clubInvitationService)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
            _clubInvitationService = clubInvitationService;
        }

        public Result<ClubJoinRequestSendDto> Send(ClubJoinRequestSendDto request)
        {
            try
            {
                var joinRequest = _mapper.Map<ClubJoinRequest>(request);
                _requestRepository.Create(joinRequest);
                return request;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
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
                    _clubInvitationService.DeleteWaiting(request.ClubId, request.TouristId);
                    _clubMemberManagementService.AddMember(request.ClubId, request.TouristId);
                }

                return Result.Ok().WithSuccess("Club Join Request " + (response.Accepted ? "Accepted" : "Rejected"));
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError("Club Join Request Not Found: " + id);
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
                return Result.Fail(FailureCode.NotFound).WithError("Club Join Request Not Found: " + id);
            }
        }

        public Result<PagedResult<ClubJoinRequestByTouristDto>> GetPagedByTourist(long id, int page, int pageSize)
        {
            var requests = _requestRepository.GetPagedByTourist(id, page, pageSize);
            return MapToDto<ClubJoinRequestByTouristDto>(requests);
        }

        public Result<PagedResult<ClubJoinRequestByClubDto>> GetPagedByClub(long id, int page, int pageSize)
        {
            var requests = _requestRepository.GetPagedByClub(id, page, pageSize);
            return MapToDto<ClubJoinRequestByClubDto>(requests);
        }

        private PagedResult<T> MapToDto<T>(PagedResult<ClubJoinRequest> requests)
        {
            var requestsDto = requests.Results.Select(_mapper.Map<T>).ToList();
            return new PagedResult<T>(requestsDto, requests.TotalCount);
        }

        public void DeletePending(long clubId, long touristId)
        {
            var requests = _requestRepository.GetAll(r => r.ClubId == clubId && r.TouristId == touristId && r.Status == ClubJoinRequestStatus.Pending);
            foreach (var request in requests)
            {
                _requestRepository.Delete(request.Id);
            }
        }

        public void DeleteByClubId(long clubId)
        {
            var requests = _requestRepository.GetAll(r => r.ClubId == clubId);
            foreach (var request in requests)
            {
                _requestRepository.Delete(request.Id);
            }
        }
    }
}
