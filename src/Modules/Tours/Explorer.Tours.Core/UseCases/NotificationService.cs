using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class NotificationService: CrudService<PublicFacilityNotificationResponseDto, PublicFacilityNotification>,INotificationService
    {
        private readonly ICrudRepository<PublicFacilityNotification> _repository;
        private readonly IPublicKeyPointNotificationRepository _keypointNotificationRepository;
        private readonly IPublicFacilityNotificationRepository _facilityNotificationRepository;

        private readonly IMapper _mapper;
        public NotificationService(ICrudRepository<PublicFacilityNotification> repository, IMapper mapper,IPublicKeyPointNotificationRepository keypointNotificationRepository,IPublicFacilityNotificationRepository facilityNotificationRepository) : base(repository, mapper)
        {
            _keypointNotificationRepository = keypointNotificationRepository;
            _facilityNotificationRepository = facilityNotificationRepository;
            _mapper = mapper;
        }
        public Result<PagedResult<PublicFacilityNotificationResponseDto>> GetFacilityNotificationsByAuthorId(int page, int pageSize, long id)
        {
            
            return MapToDto<PublicFacilityNotificationResponseDto>(_facilityNotificationRepository.GetByAuthorId(page, pageSize, id));
        }
        public Result<PagedResult<PublicKeyPointNotificationResponseDto>> GetKeyPointNotificationsByAuthorId(int page, int pageSize, long id)
        {
            PagedResult<PublicKeyPointNotification> notifications = _keypointNotificationRepository.GetByAuthorId(page,pageSize,id);
            var result = new PagedResult<PublicKeyPointNotification>(notifications.Results, notifications.TotalCount);
            var items = result.Results.Select(_mapper.Map<PublicKeyPointNotificationResponseDto>).ToList();
            return new PagedResult<PublicKeyPointNotificationResponseDto>(items, result.TotalCount);
            // return MapToDto<PublicKeyPointNotificationResponseDto>(_keypointNotificationRepository.GetByAuthorId(page, pageSize, id));
        }

    }
}
