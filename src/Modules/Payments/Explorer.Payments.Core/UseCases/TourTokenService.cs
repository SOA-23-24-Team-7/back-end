using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
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
        private readonly ICrudRepository<Tour> _tourRepository;
        private readonly ICrudRepository<Record> _recordRepository;
        IMapper _mapper;
        public TourTokenService(ICrudRepository<TourToken> repository, IMapper mapper, ICrudRepository<Tour> tourRepository, ICrudRepository<Record> recordRepository) : base(repository, mapper)
        {
            _repository = repository;
            _tourRepository = tourRepository;
            _mapper = mapper;
            _recordRepository = recordRepository;
        }

        public Result<TourTokenResponseDto> AddToken(TourTokenCreateDto token)
        {
            //check if tour is archived
            try
            {
                var tour = _tourRepository.Get(token.TourId);
                if (tour == null || tour.Status == TourStatus.Archived) //OVDE JE PRE PISALO TOURS.DOMAIN
                {
                    return Result.Fail(FailureCode.InvalidArgument);
                }

                if (_repository.GetAll().Find(tk => tk.TourId == token.TourId && tk.TouristId == token.TouristId) != null)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Tour already bought");
                }
                var newToken = _repository.Create(MapToDomain<TourTokenCreateDto>(token));
                var newRecord=CreateRecord(token.TouristId, token.TourId, tour.Price);
                if(newRecord == null)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Error in creating record");
                }

                //kreirati record

                return MapToDto<TourTokenResponseDto>(newToken);
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

    }
}
