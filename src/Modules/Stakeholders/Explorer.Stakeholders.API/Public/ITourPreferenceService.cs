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
        Result<TourPreferenceDto> Create(TourPreferenceDto tourPreferencesDto);
        Result<TourPreferenceDto> GetByUserId(int id);
        Result Delete(int id);
        Result<TourPreferenceDto> Update(TourPreferenceDto tourPreferencesDto);
    }
}
