using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class TourRecommendersService : BaseService<Tour>, IToursRecommendersService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITouristPositionRepository _touristPositionRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        private readonly IMapper _mapper;
        public TourRecommendersService(IMapper mapper, ITourRepository tourRepository, IKeyPointRepository keyPointRepository, ITouristPositionRepository touristPositionRepository) : base (mapper)
        {
            _tourRepository = tourRepository;
            _keyPointRepository = keyPointRepository;
            _mapper = mapper;
            _touristPositionRepository = touristPositionRepository;
        }

        public Result<PagedResult<TourResponseDto>> GetActiveTours(long touristId)
        {
            TouristPosition position = _touristPositionRepository.GetByTouristId(touristId);
            if(position == null)
            {
                position = new TouristPosition(touristId, 18.2, 45.8);
            }
            var tours = _tourRepository.GetPublishedTours(0, 0);
            List<Tour> nearbyTours = new List<Tour>();
            foreach(var tour in tours.Results)
            {
                var keyPoints = _keyPointRepository.GetByTourId(tour.Id);
                var nearbyKeypoints = keyPoints.Where(k => k.CalculateDistance(position.Longitude, position.Latitude) <= 40000);
                if (nearbyKeypoints.Any())
                {
                    nearbyTours.Add(tour);
                }
                if(nearbyTours.Count > 9)
                {
                    break;
                }
            }
            var activeTours = MapToResponseDto(nearbyTours);
            return new PagedResult<TourResponseDto>(activeTours, activeTours.Count);
        }

        public Result<PagedResult<TourResponseDto>> GetRecommendedTours(long touristId)
        {
            TouristPosition position = _touristPositionRepository.GetByTouristId(touristId);
            var tours = _tourRepository.GetPublishedTours(0, 0);
            List<Tour> nearbyTours = new List<Tour>();
            foreach (var tour in tours.Results)
            {
                var keyPoints = _keyPointRepository.GetByTourId(tour.Id);
                var nearbyKeypoints = keyPoints.Where(k => k.CalculateDistance(position.Longitude, position.Latitude) <= 40000);
                if (nearbyKeypoints.Any())
                {
                    nearbyTours.Add(tour);
                }
                if (nearbyTours.Count > 9)
                {
                    break;
                }
            }
            var activeTours = MapToResponseDto(nearbyTours);
            return new PagedResult<TourResponseDto>(activeTours, activeTours.Count);
        }

        private List<TourResponseDto> MapToResponseDto(List<Tour> tours)
        {
            List<TourResponseDto> mapped = new List<TourResponseDto>();
            foreach (var tour in tours)
            {
                var dto = _mapper.Map<TourResponseDto>(tour);
                foreach (var keyPoint in tour.KeyPoints)
                {
                    dto.KeyPoints.Add(_mapper.Map<KeyPointResponseDto>(keyPoint));
                }
                mapped.Add(_mapper.Map<TourResponseDto>(tour));
            }
            return mapped;
        }

    }
}
