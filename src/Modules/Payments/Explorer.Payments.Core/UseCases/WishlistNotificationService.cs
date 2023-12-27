using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using FluentResults;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class WishlistNotificationService: CrudService<WishlistNotificationResponseDto, WishlistNotification>, IWishlistNotificationService
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<WishlistNotification> _repository;

        public WishlistNotificationService(IMapper mapper, ICrudRepository<WishlistNotification> repository): base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Result<List<WishlistNotificationResponseDto>> GetByTouristId(long touristId)
        {
            var touristNotifications =  _repository.GetAll().FindAll(n => n.TouristId == touristId);
            List<WishlistNotificationResponseDto> returnValues = new List<WishlistNotificationResponseDto>();
            foreach(var t in touristNotifications)
            {
                var notification = new WishlistNotificationResponseDto(t.TourId, t.TouristId, t.Description);
                returnValues.Add(notification);
            }
            return Result.Ok(returnValues);

        }
    }
}
