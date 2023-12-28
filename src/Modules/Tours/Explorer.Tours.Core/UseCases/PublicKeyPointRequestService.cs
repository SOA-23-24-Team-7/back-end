using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
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
        private readonly IInternalUserService _userService;

        public PublicKeyPointRequestService(ICrudRepository<PublicKeyPointRequest> repository, ICrudRepository<PublicKeyPoint> publicKeyPointRepository, ICrudRepository<PublicKeyPointNotification> notificationRepository, IKeyPointRepository keyPointRepository, IMapper mapper, IInternalUserService userService) : base(repository, mapper)
        {
            _repository = repository;
            _notificationRepository = notificationRepository;
            _keyPointRepository = keyPointRepository;
            _userService = userService;
            _publicKeyPointRepository = publicKeyPointRepository;
        }
        public Result Reject(long requestId, string comment, long adminId)
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
                CreateNotification(request, false, adminId);

                return Result.Ok().WithSuccess("Request rejected successfully.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
            }
        }

        //ToDo:add name and picture here
        private void CreateNotification(PublicKeyPointRequest request, bool isAccepted, long adminId)
        {
            var keyPoint = _keyPointRepository.Get(request.KeyPointId);
            string notificationText;
            string notificationHeader;

            if (isAccepted)
            {
                notificationText = NotificationGenerator.GenerateAccepted(keyPoint.Name);
                notificationHeader = NotificationGenerator.GenerateApprovalHeader();
                request.Comment = "";
            }
            else
            {
                notificationText = NotificationGenerator.GenerateRejected(keyPoint.Name);
                notificationHeader = NotificationGenerator.GenerateRejectionHeader();
            }

            string senderPicture = _userService.GetProfilePicture(adminId);
            string senderName = _userService.GetNameById(adminId).Value;
            _notificationRepository.Create(new PublicKeyPointNotification(notificationText, request.AuthorId, request.Id, DateTime.UtcNow, isAccepted, request.Comment, senderPicture, senderName, notificationHeader));
        }

        public Result Accept(long requestId, long adminId)
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

                CreateNotification(request, true, adminId);

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
            _publicKeyPointRepository.Create(new PublicKeyPoint(keyPoint.Name, keyPoint.Description, keyPoint.Longitude, keyPoint.Latitude, keyPoint.ImagePath, keyPoint.Order, keyPoint.LocationAddress));
        }

        public Result<PagedResult<PublicKeyPointRequestResponseDto>> GetPagedWithName(int page, int pageSize)
        {
            var result = _repository.GetPaged(page, pageSize).Results;
            var resultsDto = new List<PublicKeyPointRequestResponseDto>();
            foreach (PublicKeyPointRequest req in result)
            {
                var dto = MapToDto<PublicKeyPointRequestResponseDto>(req);
                dto.KeyPointName = _keyPointRepository.Get(req.KeyPointId).Name;
                dto.Author = _userService.Get(req.AuthorId).Value;
                resultsDto.Add(dto);
            }
            resultsDto.Reverse();
            return new PagedResult<PublicKeyPointRequestResponseDto>(resultsDto, resultsDto.Count);
        }
    }
}
