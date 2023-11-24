using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Utilities;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class PublicFacilityRequestService : CrudService<PublicFacilityRequestResponseDto, PublicFacilityRequest>, IPublicFacilityRequestService
    {
        private readonly ICrudRepository<PublicFacilityRequest> _repository;
        private readonly ICrudRepository<PublicFacilityNotification> _notificationRepository;
        private readonly ICrudRepository<Facility> _facilityRepository;
        public PublicFacilityRequestService(ICrudRepository<PublicFacilityRequest> repository, IMapper mapper, ICrudRepository<PublicFacilityNotification> notificationRepository, ICrudRepository<Facility> facilityRepository) : base(repository, mapper)
        {
            _repository = repository;
            _notificationRepository = notificationRepository;
            _facilityRepository = facilityRepository;
        }
        public Result Reject(long requestId, string comment)
        {
            try
            {
                var request = _repository.Get(requestId);

                if (!isPending(request))
                    return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);

                request.Status = PublicStatus.Rejected;
                request.Comment = comment;

                _repository.Update(request);
                CreateNotification(request, false);

                return Result.Ok().WithSuccess("Request rejected successfully.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
            }
        }

        //ToDo:add name and picture here
        private void CreateNotification(PublicFacilityRequest request, bool isAccepted)
        {
            var facility = _facilityRepository.Get(request.FacilityId);
            NotificationGenerator generator = new NotificationGenerator();
            string notificationText;

            if (isAccepted)
            {
                notificationText = generator.GenerateAccepted(facility.Name);
                request.Comment = "";
            }
            else
            {
                notificationText = generator.GenerateRejected(facility.Name);
            }

            _notificationRepository.Create(new PublicFacilityNotification(notificationText, request.AuthorId, request.Id, DateTime.UtcNow, isAccepted, request.Comment));
        }

        public Result Accept(long requestId)
        {
            try
            {
                var request = _repository.Get(requestId);

                if (!isPending(request))
                    return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);

                request.Status = PublicStatus.Accepted;

                _repository.Update(request);
                CreateNotification(request, true);

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
                resultsDto.Add(dto);
            }
            resultsDto.Reverse();
            return new PagedResult<PublicFacilityRequestResponseDto>(resultsDto, resultsDto.Count);
        }
    }
}
