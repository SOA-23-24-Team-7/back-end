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
    public class CampaignService : BaseService<Campaign>, ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly ITourRepository _tourRepository;
        public CampaignService(IMapper mapper, ICampaignRepository campaignRepository, ITourRepository tourRepository) : base(mapper)
        {
            _campaignRepository = campaignRepository;
            _tourRepository = tourRepository;
        }
        public Result<CampaignResponseDto> CreateCampaign(CampaignCreateDto createDto)
        {
            List<Tour> tours = new List<Tour>();
            foreach (long TourId in createDto.TourIds)
            {
                Tour tour = _tourRepository.GetById(TourId);
                if (tour != null)
                    tours.Add(tour);
                else
                    return null;
            }
            Campaign campaign = new Campaign(createDto.TouristId, createDto.Name, createDto.Description, tours);
            _campaignRepository.Save(campaign);
            return MapToDto<CampaignResponseDto>(campaign);
        }
        public Result<List<CampaignResponseDto>> GetTouristCampaigns(long touristId)
        {
            List<Campaign> campaigns = _campaignRepository.GetByTouristId(touristId);
            return MapToDto<CampaignResponseDto>(campaigns);
        }
        public Result<TourCampaignResponseDto> GetById(long Id)
        {
            Campaign campaign = _campaignRepository.GetById(Id);
            if (campaign == null)
                return null;
            return MapToDto<TourCampaignResponseDto>(campaign);
        }
    }
}
