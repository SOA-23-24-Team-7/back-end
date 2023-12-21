using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Utilities;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class PublicFacilityRequestService : CrudService<PublicFacilityRequestResponseDto, PublicFacilityRequest>, IPublicFacilityRequestService
    {
        private readonly ICrudRepository<PublicFacilityRequest> _repository;
        private readonly ICrudRepository<PublicFacilityNotification> _notificationRepository;
        private readonly ICrudRepository<Facility> _facilityRepository;
        private readonly IInternalUserService _userService;

        public PublicFacilityRequestService(ICrudRepository<PublicFacilityRequest> repository, IMapper mapper, ICrudRepository<PublicFacilityNotification> notificationRepository, ICrudRepository<Facility> facilityRepository, IInternalUserService userService) : base(repository, mapper)
        {
            _repository = repository;
            _notificationRepository = notificationRepository;
            _facilityRepository = facilityRepository;
            _userService = userService;
        }
        public Result Reject(long requestId, string comment, long adminId)
        {
            try
            {
                var request = _repository.Get(requestId);

                if (!isPending(request))
                    return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);

                request.Status = PublicStatus.Rejected;
                request.Comment = comment;

                _repository.Update(request);
                CreateNotification(request, false, adminId);

                return Result.Ok().WithSuccess("Request rejected successfully.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
            }
        }

        //ToDo:add name and picture here
        private void CreateNotification(PublicFacilityRequest request, bool isAccepted, long adminId)
        {
            var facility = _facilityRepository.Get(request.FacilityId);
            string notificationText;
            string notificationHeader;

            if (isAccepted)
            {
                notificationText = NotificationGenerator.GenerateAccepted(facility.Name);
                notificationHeader = NotificationGenerator.GenerateApprovalHeader();
                request.Comment = "";
            }
            else
            {
                notificationText = NotificationGenerator.GenerateRejected(facility.Name);
                notificationHeader = NotificationGenerator.GenerateRejectionHeader();
            }

            var senderPicture = _userService.GetProfilePicture(adminId);
            var senderName = _userService.GetNameById(adminId).Value;
            _notificationRepository.Create(new PublicFacilityNotification(notificationText, request.AuthorId, request.Id, DateTime.UtcNow, isAccepted, request.Comment, senderPicture, senderName, notificationHeader));
        }

        public Result Accept(long requestId, long adminId)
        {
            try
            {
                var request = _repository.Get(requestId);

                if (!isPending(request))
                    return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);

                request.Status = PublicStatus.Accepted;

                _repository.Update(request);
                CreateNotification(request, true, adminId);

                return Result.Ok().WithSuccess("Request accepted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
            }
        }

        private bool isPending(PublicFacilityRequest request)
        {
            return request.Status == PublicStatus.Pending;
        }

        public Result<PagedResult<PublicFacilityRequestResponseDto>> GetPagedWithName(int page, int pageSize)
        {
            var result = _repository.GetPaged(page, pageSize).Results;
            var resultsDto = new List<PublicFacilityRequestResponseDto>();
            foreach (PublicFacilityRequest req in result)
            {
                var dto = MapToDto<PublicFacilityRequestResponseDto>(req);
                dto.FacilityName = _facilityRepository.Get(req.FacilityId).Name;
                dto.Author = _userService.Get(req.AuthorId).Value;
                resultsDto.Add(dto);
            }
            resultsDto.Reverse();
            return new PagedResult<PublicFacilityRequestResponseDto>(resultsDto, resultsDto.Count);

            
        }
    }
}
