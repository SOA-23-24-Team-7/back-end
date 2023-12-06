using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ICampaignService
    {
        Result<CampaignResponseDto> CreateCampaign(CampaignCreateDto createDto);
        Result<TourCampaignResponseDto> GetById(long Id);
        Result<List<CampaignResponseDto>> GetTouristCampaigns(long touristId);
    }
}
