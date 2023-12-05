using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ShoppingNotificationService : CrudService<ShoppingNotificationResponseDto, ShoppingNotification>, IShoppingNotificationService
    {
        private readonly IMapper _mapper;
        private readonly IShoppingNotificationRepository _notificationRepository;
        public ShoppingNotificationService(ICrudRepository<ShoppingNotification> repository, IShoppingNotificationRepository notificationRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public Result<PagedResult<ShoppingNotificationResponseDto>> GetByTouristId(int page, int pageSize, long tourId)
        {
            var pagedReviews = _notificationRepository.GetByTouristId(page, pageSize, tourId);
            var result = MapToDto<ShoppingNotificationResponseDto>(pagedReviews);
            return result;
        }
        public Result<ShoppingNotificationResponseDto> SetSeenStatus(long id)
        {
            try
            {
                ShoppingNotification notification = CrudRepository.Get(id);
                if (!notification.HasSeen)
                {
                    notification.SetSeenStatus();
                    var result = CrudRepository.Update(notification);
                    return MapToDto<ShoppingNotificationResponseDto>(result);
                }
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<int> CountUnseenNotifications(long userId)
        {
            try
            {
                int count = 0;
                count += _notificationRepository.CountNotSeen(userId);

                return count;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
