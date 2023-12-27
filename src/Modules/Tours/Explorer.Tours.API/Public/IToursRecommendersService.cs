using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IToursRecommendersService
    {
        Result<PagedResult<TourResponseDto>> GetRecommendedTours(long touristId);
        Result<PagedResult<TourResponseDto>> GetActiveTours(long touristId);
        List<TourResponseDto> GetActiveToursList(long touristId);

        List<TourResponseDto> GetRecommendedToursForMail(long touristId);
    }
}
