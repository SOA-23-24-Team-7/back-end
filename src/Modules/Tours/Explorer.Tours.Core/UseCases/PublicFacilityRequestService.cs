using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
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
        public Result Reject(long requestId,string comment)
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

        private void CreateNotification(PublicFacilityRequest request, bool isAccepted)
        {
            var facility = _facilityRepository.Get(request.FacilityId);
            NotificationGenerator generator = new NotificationGenerator();
            string notificationText;

            if (isAccepted)
                notificationText = generator.GenerateAccepted(facility.Name);
            
            else
                notificationText = generator.GenerateRejected(facility.Name,request.Comment);

            _notificationRepository.Create(new PublicFacilityNotification(notificationText, request.AuthorId, request.Id));
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
                CreateNotification(request, false);

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
    }
}
