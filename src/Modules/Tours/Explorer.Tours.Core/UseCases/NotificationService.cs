using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class NotificationService : CrudService<PublicFacilityNotificationResponseDto, PublicFacilityNotification>, INotificationService, IInternalNotificationService
    {
        private readonly ICrudRepository<PublicFacilityNotification> _repository;
        private readonly IPublicKeyPointNotificationRepository _keypointNotificationRepository;
        private readonly IPublicFacilityNotificationRepository _facilityNotificationRepository;

        private readonly IMapper _mapper;
        public NotificationService(ICrudRepository<PublicFacilityNotification> repository, IMapper mapper, IPublicKeyPointNotificationRepository keypointNotificationRepository, IPublicFacilityNotificationRepository facilityNotificationRepository) : base(repository, mapper)
        {
            _keypointNotificationRepository = keypointNotificationRepository;
            _facilityNotificationRepository = facilityNotificationRepository;
            _mapper = mapper;
        }
        public Result<PagedResult<PublicFacilityNotificationResponseDto>> GetFacilityNotificationsByAuthorId(int page, int pageSize, long id)
        {
            PagedResult<PublicFacilityNotification> notifications = _facilityNotificationRepository.GetByAuthorId(page, pageSize, id);
            notifications.Results.Where(x => !x.IsSeen && x.AuthorId == id).ToList().ForEach(x =>
            {
                x.SetSeenStatus();
                _facilityNotificationRepository.Update(x);
            });
            var result = new PagedResult<PublicFacilityNotification>(notifications.Results, notifications.TotalCount);
            var items = result.Results.Select(_mapper.Map<PublicFacilityNotificationResponseDto>).ToList();
            items.Reverse();
            return new PagedResult<PublicFacilityNotificationResponseDto>(items, result.TotalCount);
        }
        public Result<PagedResult<PublicKeyPointNotificationResponseDto>> GetKeyPointNotificationsByAuthorId(int page, int pageSize, long id)
        {
            PagedResult<PublicKeyPointNotification> notifications = _keypointNotificationRepository.GetByAuthorId(page, pageSize, id);
            notifications.Results.Where(x => !x.IsSeen && x.AuthorId == id).ToList().ForEach(x =>
            {
                x.SetSeenStatus();
                _keypointNotificationRepository.Update(x);
            });
            var result = new PagedResult<PublicKeyPointNotification>(notifications.Results, notifications.TotalCount);
            var items = result.Results.Select(_mapper.Map<PublicKeyPointNotificationResponseDto>).ToList();
            items.Reverse();
            return new PagedResult<PublicKeyPointNotificationResponseDto>(items, result.TotalCount);
        }

        public int CountNotSeen(long userId)
        {
            int count = 0;
            count += _facilityNotificationRepository.CountNotSeen(userId);
            count += _keypointNotificationRepository.CountNotSeen(userId);

            return count;
        }
    }
}
