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
    public class PublicKeyPointRequestService : CrudService<PublicKeyPointRequestResponseDto, PublicKeyPointRequest>, IPublicKeyPointRequestService
    {
        private readonly ICrudRepository<PublicKeyPointRequest> _repository;
        private readonly ICrudRepository<PublicKeyPointNotification> _notificationRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        private readonly ICrudRepository<PublicKeyPoint> _publicKeyPointRepository;
        public PublicKeyPointRequestService(ICrudRepository<PublicKeyPointRequest> repository, ICrudRepository<PublicKeyPoint> publicKeyPointRepository, ICrudRepository<PublicKeyPointNotification> notificationRepository, IKeyPointRepository keyPointRepository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _notificationRepository = notificationRepository;
            _keyPointRepository = keyPointRepository;
            _publicKeyPointRepository = publicKeyPointRepository;
        }
        public Result Reject(long requestId,string comment)
        {
            try
            {
                var request = _repository.Get(requestId);

                if (!isPending(request))
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
                }

                request.Status = PublicStatus.Rejected;
                request.Comment = comment;

                _repository.Update(request);
                CreateNotification(request,false);

                return Result.Ok().WithSuccess("Request rejected successfully.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
            }
        }
        private void CreateNotification(PublicKeyPointRequest request, bool isAccepted)
        {
            var keyPoint = _keyPointRepository.Get(request.KeyPointId);
            NotificationGenerator generator = new NotificationGenerator();
            string notificationText;
            if (isAccepted)
            {
                notificationText = generator.GenerateAccepted(keyPoint.Name);
            }
            else
            {
                notificationText = generator.GenerateRejected(keyPoint.Name,request.Comment);
            }
            _notificationRepository.Create(new PublicKeyPointNotification(notificationText, request.AuthorId, request.Id));
        }

        public Result Accept(long requestId)
        {
            try
            {
                var request = _repository.Get(requestId);

                if (!isPending(request))
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
                }

                request.Status = PublicStatus.Accepted;
                _repository.Update(request);

                CreatePublicKeypoint(request);

                CreateNotification(request, true);

                return Result.Ok().WithSuccess("Request accepted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
            }
        }
    
        private bool isPending(PublicKeyPointRequest request)
        {
            return request.Status == PublicStatus.Pending;
        }

        private void CreatePublicKeypoint(PublicKeyPointRequest request)
        {
            //dobavljanje keypointa za koji je poslat zahtjev, preko njega pravim public keypoint
            var keyPoint = _keyPointRepository.Get(request.KeyPointId);
            _publicKeyPointRepository.Create(new PublicKeyPoint(keyPoint.Name, keyPoint.Description, keyPoint.Longitude, keyPoint.Latitude, keyPoint.ImagePath, keyPoint.Order));
        }
     
    }
}
