using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.Tours; //GREH , kliknula sam -  add reference 
using FluentResults;
using System.Diagnostics;

namespace Explorer.Payments.Core.UseCases
{
    public class TourTokenService : CrudService<TourTokenResponseDto, TourToken>, ITourTokenService, IInternalTourTokenService
    {
        private readonly ICrudRepository<TourToken> _repository;
        private readonly ICrudRepository<Record> _recordRepository;
        private readonly ICrudRepository<ShoppingNotification> _shoppingNotificationRepository;
        private readonly IWalletService _walletService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private IInternalTourService _tourService;
        private readonly IBundleRepository _bundleRepository;
        private readonly IBundleRecordRepository _bundleRecordRepository;
        IMapper _mapper;
        public TourTokenService(ICrudRepository<TourToken> repository, IMapper mapper, ICrudRepository<Record> recordRepository, IWalletService walletService,IShoppingCartRepository shoppingCartRepository, IInternalTourService tourService, ICrudRepository<ShoppingNotification> shoppingNotificationRepository, IBundleRepository bundleRepository, IBundleRecordRepository bundleRecordRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _recordRepository = recordRepository;
            _walletService = walletService;
            _shoppingCartRepository = shoppingCartRepository;
            _tourService = tourService;
            _shoppingNotificationRepository = shoppingNotificationRepository;
            _bundleRepository = bundleRepository;
            _bundleRecordRepository = bundleRecordRepository;
        }

        public Result<TourTokenResponseDto> AddToken(TourTokenCreateDto token, long totalPrice, long orderItemPrice)
        {
            //check if tour is archived
            try
            {
                var wallet = _walletService.GetForTourist(token.TouristId);
                var shoppingCart = _shoppingCartRepository.GetByTouristId(token.TouristId);
                var tour = _tourService.Get(token.TourId)?.Value;
                
                if (wallet.Value.AdventureCoin >= totalPrice)
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
                    var newRecord = CreateRecord(token.TouristId, token.TourId, orderItemPrice);
                    CreateNotfication(token.TouristId, token.TourId);
                    wallet.Value.AdventureCoin -= orderItemPrice;    //tu je snizena cijena!
                    _walletService.Update(new WalletUpdateDto(wallet.Value.Id,wallet.Value.AdventureCoin));
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

        public Result AddTokensByBundle(long touristId, long bundleId)
        {
            try
            {
                var wallet = _walletService.GetForTourist(touristId);
                var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);
                var bundle = _bundleRepository.Get(b => b.Id == bundleId, include: "BundleItems");
                if (wallet.Value.AdventureCoin >= shoppingCart.TotalPrice)
                {
                    foreach(var bundleItem in bundle.BundleItems)
                    {
                        var tour = _tourService.Get(bundleItem.TourId)?.Value;
                        if (tour == null || (TourStatus)tour.Status == TourStatus.Archived)
                        {
                            return Result.Fail(FailureCode.InvalidArgument);
                        }

                        var token = _repository.Create(MapToDomain<TourTokenCreateDto>(new TourTokenCreateDto { TourId = tour.Id, TouristId = touristId }));
                        CreateNotfication(token.TouristId, token.TourId);
                    }


                    wallet.Value.AdventureCoin -= bundle.Price;
                    _walletService.Update(new WalletUpdateDto(wallet.Value.Id, wallet.Value.AdventureCoin));
                    _bundleRecordRepository.Create(new BundleRecord(touristId, bundle.Id, bundle.Price));
                    return Result.Ok();
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

        public long GetTourTokenCount(long tourId)
        {
            long count = _repository.GetAll(tk => tk.TourId == tourId).Count();
            return count;
        }
        public  Result<List<TourTokenResponseDto>> GetAll()
        {
            return MapToDto<TourTokenResponseDto>(_repository.GetAll());
        }

        public List<long> GetPurchasedToursIds(long touristId)
        {
            var tokens = _repository.GetAll().FindAll(token => token.TouristId == touristId);
            return tokens.Select(token => token.TourId).ToList();
        }

    }
}
