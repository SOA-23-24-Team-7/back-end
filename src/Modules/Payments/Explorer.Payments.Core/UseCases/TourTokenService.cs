using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.Tours; //GREH , kliknula sam -  add reference 
using FluentResults;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TourTokenService : CrudService<TourTokenResponseDto, TourToken>, ITourTokenService
    {
        private readonly ICrudRepository<TourToken> _repository;
        private readonly ICrudRepository<Record> _recordRepository;
        private readonly ICrudRepository<ShoppingNotification> _shoppingNotificationRepository;
        private readonly IWalletService _walletService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private IInternalTourService _tourService;
        IMapper _mapper;
        public TourTokenService(ICrudRepository<TourToken> repository, IMapper mapper, ICrudRepository<Record> recordRepository, IWalletService walletService,IShoppingCartRepository shoppingCartRepository, IInternalTourService tourService, ICrudRepository<ShoppingNotification> shoppingNotificationRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _recordRepository = recordRepository;
            _walletService = walletService;
            _shoppingCartRepository = shoppingCartRepository;
            _tourService = tourService;
            _shoppingNotificationRepository= shoppingNotificationRepository;
        }

        public Result<TourTokenResponseDto> AddToken(TourTokenCreateDto token)
        {
            //check if tour is archived
            try
            {
                var wallet = _walletService.GetForTourist(token.TouristId);
                var shoppingCart = _shoppingCartRepository.GetByTouristId(token.TouristId);
                var tour = _tourService.Get(token.TourId)?.Value;
                if (wallet.Value.AdventureCoin >= shoppingCart.TotalPrice)
                {
                    if (tour == null || (TourStatus)tour.Status == TourStatus.Archived) //OVDE JE PRE PISALO TOURS.DOMAIN
                    {
                        return Result.Fail(FailureCode.InvalidArgument);
                    }

                    if (_repository.GetAll().Find(tk => tk.TourId == token.TourId && tk.TouristId == token.TouristId) != null)
                    {
                        return Result.Fail(FailureCode.InvalidArgument).WithError("Tour already bought");
                    }
                    var newToken = _repository.Create(MapToDomain<TourTokenCreateDto>(token));
                    var newRecord = CreateRecord(token.TouristId, token.TourId, tour.Price);
                    CreateNotfication(token.TouristId, token.TourId);
                    if (newRecord == null)
                    {
                        return Result.Fail(FailureCode.InvalidArgument).WithError("Error in creating record");
                    }
                    return MapToDto<TourTokenResponseDto>(newToken);
                }
                else
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError("You don't have enough coins.");
                }
                
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }

        public Result<List<TourTokenResponseDto>> GetTouristsTokens(long touristId)
        {
            var tokens = _repository.GetAll().FindAll(token => token.TouristId == touristId);
            return MapToDto<TourTokenResponseDto>(tokens);
        }

        public Result<List<long>> GetTouristToursId(long touristId)
        {
            var tokens = _repository.GetAll().FindAll(token => token.TouristId == touristId);
            return tokens.Select(token => token.TourId).ToList();
        }

        private Record CreateRecord(long touristId, long tourId, double price)
        {
            return _recordRepository.Create(new Record(touristId, tourId, price)); 
        }
        private void CreateNotfication(long touristId, long tourId)
        {
            var tour= _tourService.Get(tourId)?.Value;
            var description="";
            if(tour!=null)
                description = "Tour " + tour.Name + " is successfully added to your tours collection.";
            _shoppingNotificationRepository.Create(new ShoppingNotification(description,touristId, tourId));
        }
    }
}
