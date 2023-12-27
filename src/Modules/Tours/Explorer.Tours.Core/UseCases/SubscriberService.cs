using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class SubscriberService : CrudService<SubscriberResponseDto, Subscriber>, ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberService(ISubscriberRepository subscriberRepository, IMapper mapper) : base(subscriberRepository, mapper)
        {
            _subscriberRepository = subscriberRepository;
        }

        public Result<SubscriberResponseDto> GetByUserId(int userId)
        {
            try
            {
                var subscriber = _subscriberRepository.Get(s => s.TouristId == userId);
                return MapToDto<SubscriberResponseDto>(subscriber);
            }
            catch (Exception ex)
            {
                var response = new SubscriberResponseDto();
                response.Frequency = 0;
                response.Id = -1;
                return response;
            }
        }

        public List<SubscriberResponseDto> GetAll()
        {
            List<SubscriberResponseDto> result = new List<SubscriberResponseDto>();
            foreach (var s in _subscriberRepository.GetAll())
            {
                result.Add(MapToDto<SubscriberResponseDto>(s));
            }
            return result;
        }

        public Result<SubscriberResponseDto> SaveOrUpdate(SubscriberCreateDto subscriberCreateDto)
        {
            if (subscriberCreateDto.Frequency != 0)
            {
                try
                {
                    var subscriber = _subscriberRepository.Get(s => s.TouristId == subscriberCreateDto.TouristId);
                    subscriber.Frequency = subscriberCreateDto.Frequency;
                    var updatedSubscriber = _subscriberRepository.Update(subscriber);
                    return MapToDto<SubscriberResponseDto>(updatedSubscriber);

                }
                catch (Exception ex)
                {
                    return Create(subscriberCreateDto);
                }
            }
            else // if (subscriberCreateDto.Frequency == 0)
            {
                try
                {
                    var subscriber = _subscriberRepository.Get(s => s.TouristId == subscriberCreateDto.TouristId);
                    Delete(subscriber.Id);
                    var response = new SubscriberResponseDto();
                    response.EmailAddress = subscriberCreateDto.EmailAddress;
                    response.Frequency = subscriberCreateDto.Frequency;
                    response.TouristId = subscriberCreateDto.TouristId;
                    response.Id = -1;
                    response.LastTimeSent = subscriberCreateDto.LastTimeSent;
                    return response;
                }
                catch (Exception e)
                {
                    var response = new SubscriberResponseDto();
                    response.EmailAddress = subscriberCreateDto.EmailAddress;
                    response.Frequency = subscriberCreateDto.Frequency;
                    response.TouristId = subscriberCreateDto.TouristId;
                    response.Id = -1;
                    response.LastTimeSent = subscriberCreateDto.LastTimeSent;
                    return response;
                }
            }

        }
    }
}
