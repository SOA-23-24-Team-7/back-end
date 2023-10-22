using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.API.Public
{
    public interface ITourPreferenceService
    {
        Result<TourPreferenceResponseDto> Create<TourPreferenceCreateDto>(TourPreferenceCreateDto tourPreferencesDto);
        Result<TourPreferenceResponseDto> GetByUserId(int id);
        Result Delete(long id);
        Result<TourPreferenceResponseDto> Update<TourPreferenceUpdateDto>(TourPreferenceUpdateDto tourPreferencesDto);
    }
}
